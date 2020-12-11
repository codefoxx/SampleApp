using SampleApp.Core;
using SampleApp.Core.Repositories;
using SampleApp.Persistence.Dapper.Repositories;
using System.Data;

namespace SampleApp.Persistence.Dapper
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly IDbConnection _db;
        private readonly IDbTransaction _transaction;

        public UnitOfWork(IDbConnection db)
        {
            _db = db;
            _transaction = _db.BeginTransaction();

            Authors = new AuthorRepository(db);
        }
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public ICourseRepository Courses { get; }
        public IAuthorRepository Authors { get; }
        public int SaveChanges()
        {
            _transaction.Commit();

            return -1;
        }
    }
}