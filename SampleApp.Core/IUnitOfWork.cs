using SampleApp.Core.Repositories;

using System;

namespace SampleApp.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository Courses { get; }
        IAuthorRepository Authors { get; }
        int SaveChanges();
    }
}