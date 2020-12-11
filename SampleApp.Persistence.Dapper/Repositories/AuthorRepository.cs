using Dapper;

using SampleApp.Core.Domain;
using SampleApp.Core.Repositories;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace SampleApp.Persistence.Dapper.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(IDbConnection db) : base(db)
        {
        }

        public override Author Get(int id)
        {
            const string sql = "SELECT * FROM Author WHERE ID = @id;";
            
            return _db.QuerySingle<Author>(sql, new {id});
        }

        public override IEnumerable<Author> GetAll()
        {
            const string sql = "SELECT * FROM Author;";
           
            return _db.Query<Author>(sql);
        }

        public override IEnumerable<Author> Find(Expression<Func<Author, bool>> predicate)
        {
            const string sql = "SELECT * FROM Author;";

            return _db.Query<Author>(sql).Where<Author>( author => author.Id == 1);
        }

        public override Author SingleOrDefault(Expression<Func<Author, bool>> predicate)
        {
            const string sql = "SELECT * FROM Author;";

            return _db.QuerySingleOrDefault<Author>(sql);
        }

        public override void Add(Author entity)
        {
            const string sql = "INSERT INTO Author (Id, Name) VALUES (@id, @name)";
            var affectedRows = _db.Execute(sql, new {id = entity.Id, name = entity.Name}, _transaction);
        }

        public override void AddRange(IEnumerable<Author> entities)
        {
            foreach (Author entity in entities)
            {
                Add(entity);
            }
        }

        public override void Remove(Author entity)
        {
            const string sql = "DELETE FROM Author WHERE Id = @id";
            var affectedRows = _db.Execute(sql, new { id = entity.Id }, _transaction);
        }

        public override void RemoveRange(IEnumerable<Author> entities)
        {
            var array = entities.Select(entity => entity.Id).ToArray();

            const string sql = "DELETE FROM Author WHERE Id IN @ids";
            var affectedRows = _db.Execute(sql, new {ids = array}, _transaction);
        }

        public Author GetAuthorWithCourses(int id)
        {
            throw new NotImplementedException();
        }
    }
}