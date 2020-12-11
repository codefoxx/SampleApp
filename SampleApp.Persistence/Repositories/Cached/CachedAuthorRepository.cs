using AutoMapper;

using Microsoft.EntityFrameworkCore.Infrastructure;

using ProtoBuf;

using SampleApp.Core.Domain;
using SampleApp.Core.Repositories;
using SampleApp.Persistence.Domain;

using StackExchange.Redis;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;

namespace SampleApp.Persistence.Repositories.Cached
{
    public class CachedAuthorRepository : IAuthorRepository
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IMapper _mapper;


        public CachedAuthorRepository(IAuthorRepository authorRepository, IConnectionMultiplexer connectionMultiplexer, IMapper mapper)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _mapper = mapper;
            _authorRepository = authorRepository;
        }


        public async Task<Author> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            RedisKey key = new RedisKey($"author_{id}");
            IDatabase db = _connectionMultiplexer.GetDatabase();

            RedisValue cachedValue = await db.StringGetAsync(key);

            if (cachedValue.HasValue)
            {
                await using var stream = new MemoryStream(cachedValue);
                var cachedAuthor = Serializer.Deserialize<CachedAuthor>(stream);

                return _mapper.Map<Author>(cachedAuthor);
            }

            var author = await _authorRepository.GetAsync(id, cancellationToken);
            await db.StringSetAsync(key, ProtoSerialize(author), TimeSpan.FromMinutes(1));

            return author;
        }

        public async Task<IReadOnlyCollection<Author>> GetAllAsync(int pageIndex = default, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            RedisKey key = new RedisKey($"authors_{pageIndex}_{pageSize}");
            IDatabase db = _connectionMultiplexer.GetDatabase();

            RedisValue cachedValue = await db.StringGetAsync(key);

            if (cachedValue.HasValue)
            {
                await using var stream = new MemoryStream(cachedValue);
                var cachedAuthors = Serializer.Deserialize<IReadOnlyCollection<CachedAuthor>>(stream);

                return _mapper.Map<IReadOnlyCollection<Author>>(cachedAuthors);
            }

            var authors = await _authorRepository.GetAllAsync(pageIndex, pageSize, cancellationToken);
            await db.StringSetAsync(key, ProtoSerialize(authors), TimeSpan.FromMinutes(1));

            return authors;
        }

        public async Task<IReadOnlyCollection<Author>> FindAsync(Expression<Func<Author, bool>> predicate, CancellationToken cancellationToken = default)
        {
            RedisKey key = new RedisKey($"authors_{predicate.Print()}");
            IDatabase db = _connectionMultiplexer.GetDatabase();

            RedisValue cachedValue = await db.StringGetAsync(key);

            if (cachedValue.HasValue)
            {
                await using var stream = new MemoryStream(cachedValue);
                var cachedAuthors = Serializer.Deserialize<IReadOnlyCollection<CachedAuthor>>(stream);

                return _mapper.Map<IReadOnlyCollection<Author>>(cachedAuthors);
            }

            var authors = await _authorRepository.FindAsync(predicate, cancellationToken);
            await db.StringSetAsync(key, ProtoSerialize(authors), TimeSpan.FromMinutes(1));

            return authors;
        }

        public async Task<Author> SingleOrDefaultAsync(Expression<Func<Author, bool>> predicate, CancellationToken cancellationToken = default)
        {
            RedisKey key = new RedisKey($"authors_{predicate.Print()}");
            IDatabase db = _connectionMultiplexer.GetDatabase();

            RedisValue cachedValue = await db.StringGetAsync(key);

            if (cachedValue.HasValue)
            {
                await using var stream = new MemoryStream(cachedValue);
                var cachedAuthor = Serializer.Deserialize<CachedAuthor>(stream);

                return _mapper.Map<Author>(cachedAuthor);
            }

            var author = await _authorRepository.SingleOrDefaultAsync(predicate, cancellationToken);
            await db.StringSetAsync(key, ProtoSerialize(author), TimeSpan.FromMinutes(1));

            return author;
        }

        public void Add(Author entity)
        {
            _authorRepository.Add(entity);
        }

        public void AddRange(IEnumerable<Author> entities)
        {
            _authorRepository.AddRange(entities);
        }

        public void Update(Author entity)
        {
            _authorRepository.Update(entity);

            RedisKey key = new RedisKey($"author_{entity.Id}");
            IDatabase db = _connectionMultiplexer.GetDatabase();

            RedisValue cachedValue = db.StringGet(key);

            if (cachedValue.HasValue)
            {
                db.SetRemove(key, cachedValue);
            }
        }

        public void Remove(Author entity)
        {
            RedisKey key = new RedisKey($"author_{entity.Id}");
            IDatabase db = _connectionMultiplexer.GetDatabase();

            RedisValue cachedValue = db.StringGet(key);

            if (cachedValue.HasValue)
            {
                db.SetRemove(key, cachedValue);
            }

            _authorRepository.Remove(entity);
        }

        public void RemoveRange(IEnumerable<Author> entities)
        {
            foreach (Author entity in entities.ToList())
            {
                Remove(entity);
            }
        }

        public async Task<Author> GetAuthorWithCourses(int id)
        {
            RedisKey key = new RedisKey($"authors_with courses_{id}");
            IDatabase db = _connectionMultiplexer.GetDatabase();

            RedisValue cachedValue = await db.StringGetAsync(key);

            if (cachedValue.HasValue)
            {
                await using var stream = new MemoryStream(cachedValue);
                var cachedAuthor = Serializer.Deserialize<CachedAuthor>(stream);

                return _mapper.Map<Author>(cachedAuthor);
            }

            var author = await _authorRepository.GetAuthorWithCourses(id);
            await db.StringSetAsync(key, ProtoSerialize(author), TimeSpan.FromMinutes(1));

            return author;
        }


        private static byte[] ProtoSerialize<T>(T record) where T : class
        {
            using var stream = new MemoryStream();
            Serializer.Serialize(stream, record);

            return stream.ToArray();
        }

        private static Stream ProtoDeserialize<T>(T record) where T : class
        {
            using var stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, record);

            return stream;
        }
    }
}