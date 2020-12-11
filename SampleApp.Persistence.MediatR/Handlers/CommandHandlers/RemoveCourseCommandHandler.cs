using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleApp.Persistence.MediatR.Commands;
using SampleApp.Persistence.Repositories;

using System.Threading;
using System.Threading.Tasks;

namespace SampleApp.Persistence.MediatR.Handlers.CommandHandlers
{
    public class RemoveCourseCommandHandler : IRequestHandler<RemoveCourseCommand>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public RemoveCourseCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Unit> Handle(RemoveCourseCommand request, CancellationToken cancellationToken)
        {
            await using var dbContext = _contextFactory.CreateDbContext();

            var unitOfWork = new UnitOfWork(dbContext);
            var repository = new CourseRepository(unitOfWork);

            var course = await repository.GetAsync(request.CourseId, cancellationToken);

            // ReSharper disable once InvertIf
            if (course != null)
            {
                repository.Remove(course);
                await unitOfWork.CommitAsync(cancellationToken);
            }

            return new Unit();
        }
    }
}