
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleApp.Core
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
        
        void Add<TEntity>(TEntity entity) where TEntity : class;

        void Update<TEntity>(TEntity entity) where TEntity : class;

        void Delete<TEntity>(TEntity entity) where TEntity : class;

        IQueryable<TEntity> Query<TEntity>() where TEntity : class;
    }
}