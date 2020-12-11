using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using SampleApp.Core;

using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleApp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly IDbContextTransaction _transaction;
        private bool _isAlive = true;
        
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            _transaction = _dbContext.Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (!_isAlive) return;

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                await _transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync(cancellationToken);
                // ReSharper disable once PossibleIntendedRethrow
                throw ex;
            }
            finally
            {
                _isAlive = false;
                _transaction.Dispose();
                await _dbContext.DisposeAsync();
            }
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }
    }
}