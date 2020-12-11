using SampleApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SampleApp.Core.Repositories
{
    public interface IRepository<TEntity, in TKey> where TEntity : IEntity<TKey>
    {
        Task<TEntity> GetAsync(TKey id, CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<TEntity>> GetAllAsync(int pageIndex = default, int pageSize = 10, CancellationToken cancellationToken = default);

        Task<IReadOnlyCollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        // This method was not in the videos, but I thought it would be useful to add.
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        void Add(TEntity entity);
        
        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Remove(TEntity entity);
        
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}