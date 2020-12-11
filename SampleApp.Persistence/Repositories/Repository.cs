using Microsoft.EntityFrameworkCore;

using SampleApp.Core;
using SampleApp.Core.Domain;
using SampleApp.Core.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SampleApp.Persistence.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity: class, IEntity<TKey>
    {
        protected readonly IUnitOfWork UnitOfWork;
        
        public Repository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public async Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await UnitOfWork.Query<TEntity>()
                .SingleOrDefaultAsync( entity => entity.Id.Equals(id), cancellationToken);
        }
        
        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(int pageIndex = 0, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            return await UnitOfWork.Query<TEntity>()
                .Skip(pageIndex)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await UnitOfWork.Query<TEntity>()
                .Where(predicate)
                .ToListAsync(cancellationToken);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await UnitOfWork.Query<TEntity>()
                .SingleOrDefaultAsync(predicate, cancellationToken);
        }

        public void Add(TEntity entity)
        {
            UnitOfWork.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                UnitOfWork.Add(entity);
            }
        }

        public void Update(TEntity entity)
        {
            UnitOfWork.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            UnitOfWork.Delete(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (TEntity entity in entities)
            {
                UnitOfWork.Delete(entity);
            }
        }
    }
}