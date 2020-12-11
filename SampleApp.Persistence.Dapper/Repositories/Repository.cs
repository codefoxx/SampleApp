using Dapper;
using SampleApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace SampleApp.Persistence.Dapper.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IDbConnection _db;
        
        protected Repository(IDbConnection db)
        {
            _db = db;
        }

        public abstract TEntity Get(int id);

        public abstract IEnumerable<TEntity> GetAll();

        public abstract IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        public abstract TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        public abstract void Add(TEntity entity);

        public abstract void AddRange(IEnumerable<TEntity> entities);

        public abstract void Remove(TEntity entity);

        public abstract void RemoveRange(IEnumerable<TEntity> entities);
    }
}