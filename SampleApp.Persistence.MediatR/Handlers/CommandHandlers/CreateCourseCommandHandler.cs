using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleApp.Core.Domain;
using SampleApp.Persistence.MediatR.Commands;
using SampleApp.Persistence.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SampleApp.Persistence.MediatR.Handlers.CommandHandlers
{
    public class CreateCourseCommandHandler: IRequestHandler<CreateCourseCommand, Course>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly IMapper _mapper;

        public CreateCourseCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<Course> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            await using var dbContext = _contextFactory.CreateDbContext();

            var unitOfWork = new UnitOfWork(dbContext);
            var repository = new CourseRepository(unitOfWork);
            var course = _mapper.Map<Course>(request);

            repository.Add(course);
            await unitOfWork.CommitAsync(cancellationToken);

            return course;
        }
    }
}