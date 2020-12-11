using MediatR;

using SampleApp.Core.Domain;

namespace SampleApp.Persistence.MediatR.Commands
{
    public class RemoveCourseCommand : IRequest<Unit>
    {
        public int CourseId { get; set; }
    }
}