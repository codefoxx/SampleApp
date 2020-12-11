using Microsoft.EntityFrameworkCore;

using SampleApp.Core;
using SampleApp.Core.Domain;
using SampleApp.Core.Repositories;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SampleApp.Persistence.Repositories
{
    public class CourseRepository : Repository<Course, int>, ICourseRepository
    {
        public CourseRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork) { }

        public async Task<IList<Course>> GetTopSellingCoursesAsync(int count, CancellationToken cancellationToken = default)
        {
            return await UnitOfWork.Query<Course>()
                .OrderByDescending(course => course.FullPrice)
                .Take(count)
                .ToListAsync(cancellationToken);
        }

        public async Task<IList<Course>> GetCoursesWithAuthorsAsync(int pageIndex = 0, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            return await UnitOfWork.Query<Course>()
                .Include(course => course.Author)
                .OrderBy(course => course.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
    }
}