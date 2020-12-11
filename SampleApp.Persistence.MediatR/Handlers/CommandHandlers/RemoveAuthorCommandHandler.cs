using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleApp.Persistence.MediatR.Commands;
using SampleApp.Persistence.Repositories;

using System.Threading;
using System.Threading.Tasks;

namespace SampleApp.Persistence.MediatR.Handlers.CommandHandlers
{
    public class RemoveAuthorCommandHandler : IRequestHandler<RemoveAuthorCommand>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public RemoveAuthorCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<Unit> Handle(RemoveAuthorCommand request, CancellationToken cancellationToken)
        {
            await using var dbContext = _contextFactory.CreateDbContext();

            var unitOfWork = new UnitOfWork(dbContext);
            var repository = new AuthorRepository(unitOfWork);

            var author = await repository.GetAsync(request.AuthorId, cancellationToken);

            // ReSharper disable once InvertIf
            if (author != null)
            {
                repository.Remove(author);
                await unitOfWork.CommitAsync(cancellationToken);
            }

            return new Unit();
        }
    }
}