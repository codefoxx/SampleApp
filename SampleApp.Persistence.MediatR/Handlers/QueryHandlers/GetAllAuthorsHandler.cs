using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleApp.Core.Domain;
using SampleApp.Persistence.MediatR.Queries;
using SampleApp.Persistence.Repositories;
using SampleApp.Persistence.Repositories.Cached;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SampleApp.Persistence.MediatR.Handlers.QueryHandlers
{
    public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<Author>>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public GetAllAuthorsHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Author>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            await using var dbContext = _contextFactory.CreateDbContext();

            var unitOfWork = new UnitOfWork(dbContext);
            var repository = new AuthorRepository(unitOfWork);

            return await repository.GetAllAsync(request.PageIndex, request.PageSize, cancellationToken);
        }
    }
}