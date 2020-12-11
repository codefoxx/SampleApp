using MediatR;

using Microsoft.EntityFrameworkCore;

using SampleApp.Core.Domain;
using SampleApp.Persistence.MediatR.Commands;
using SampleApp.Persistence.Repositories;

using System.Threading;
using System.Threading.Tasks;

namespace SampleApp.Persistence.MediatR.Handlers.CommandHandlers
{
    public class UpdateCourseCommandHandler :IRequestHandler<UpdateCourseCommand, Course>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public UpdateCourseCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<Course> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            await using var dbContext = _contextFactory.CreateDbContext();

            var unitOfWork = new UnitOfWork(dbContext);
            var repository = new CourseRepository(unitOfWork);

            var course = await repository.GetAsync(request.Id, cancellationToken);
            if (course == null)
            {
                return null;
            }

            course.Name = request.Name;
            course.Author = request.Author;
            course.Description = request.Description;
            course.FullPrice = request.FullPrice;
            course.Tags = request.Tags;
            
            repository.Update(course);
            await unitOfWork.CommitAsync(cancellationToken);

            return course;
        }
    }
}