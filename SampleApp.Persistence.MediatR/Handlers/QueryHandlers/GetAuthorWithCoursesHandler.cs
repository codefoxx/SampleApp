using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleApp.Core.Domain;
using SampleApp.Persistence.MediatR.Queries;
using SampleApp.Persistence.Repositories;

using System.Threading;
using System.Threading.Tasks;

namespace SampleApp.Persistence.MediatR.Handlers.QueryHandlers
{
    public class GetAuthorWithCoursesHandler : IRequestHandler<GetAuthorWithCoursesQuery, Author>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public GetAuthorWithCoursesHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<Author> Handle(GetAuthorWithCoursesQuery request, CancellationToken cancellationToken)
        {
            await using var dbContext = _contextFactory.CreateDbContext();

            var unitOfWork = new UnitOfWork(dbContext);
            var repository = new AuthorRepository(unitOfWork);

            return await repository.GetAuthorWithCourses(request.AuthorId);
        }
    }
}