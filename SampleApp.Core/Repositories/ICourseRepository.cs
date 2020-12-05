using SampleApp.Core.Domain;

using System.Collections.Generic;

namespace SampleApp.Core.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {
        IEnumerable<Course> GetTopSellingCourses(int count);
        IEnumerable<Course> GetCoursesWithAuthors(int pageIndex = 0, int pageSize = 10);
    }
}