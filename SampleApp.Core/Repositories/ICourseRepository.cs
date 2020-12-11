using SampleApp.Core.Domain;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SampleApp.Core.Repositories
{
    public interface ICourseRepository : IRepository<Course, int>
    {
        Task<IList<Course>> GetTopSellingCoursesAsync(int count, CancellationToken cancellationToken = default);

        Task<IList<Course>> GetCoursesWithAuthorsAsync(int pageIndex = 0, int pageSize = 10, CancellationToken cancellationToken = default);
    }
}