using Microsoft.EntityFrameworkCore;

using SampleApp.Core.Domain;
using SampleApp.Core.Repositories;

using System.Collections.Generic;
using System.Linq;

namespace SampleApp.Persistence.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(DbContext context) : base(context)
        {
        }

        public SampleAppContext SampleAppContext => Context as SampleAppContext;

        public IEnumerable<Course> GetTopSellingCourses(int count)
        {
            return SampleAppContext.Courses
                .OrderByDescending(c => c.FullPrice)
                .Take(count)
                .ToList();
        }

        public IEnumerable<Course> GetCoursesWithAuthors(int pageIndex, int pageSize)
        {
            return SampleAppContext.Courses
                .Include(c => c.Author)
                .OrderBy(c => c.Name)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}