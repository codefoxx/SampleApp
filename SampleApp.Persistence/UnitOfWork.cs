using SampleApp.Core;
using SampleApp.Core.Repositories;
using SampleApp.Persistence.Repositories;

namespace SampleApp.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SampleAppContext _context;

        public UnitOfWork(SampleAppContext context)
        {
            _context = context;
            Courses = new CourseRepository(_context);
            Authors = new AuthorRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public ICourseRepository Courses { get; }
        public IAuthorRepository Authors { get; }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}