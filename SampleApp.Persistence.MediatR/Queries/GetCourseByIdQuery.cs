using MediatR;
using SampleApp.Core.Domain;

namespace SampleApp.Persistence.MediatR.Queries
{
    public class GetCourseByIdQuery :IRequest<Course>
    {
        public int CourseId { get; set; }
    }
}