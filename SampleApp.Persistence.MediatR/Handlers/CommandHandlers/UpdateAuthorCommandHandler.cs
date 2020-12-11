using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleApp.Core.Domain;
using SampleApp.Persistence.MediatR.Commands;
using SampleApp.Persistence.Repositories;

using System.Threading;
using System.Threading.Tasks;

namespace SampleApp.Persistence.MediatR.Handlers.CommandHandlers
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Author>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public UpdateAuthorCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            await using var dbContext = _contextFactory.CreateDbContext();

            var unitOfWork = new UnitOfWork(dbContext);
            var repository = new AuthorRepository(unitOfWork);

            var author = await repository.GetAsync(request.Id, cancellationToken);
            if (author == null)
            {
                return null;
            }

            author.Name = request.Name;
            author.Courses = request.Courses;

            repository.Update(author);
            await unitOfWork.CommitAsync(cancellationToken);

            return author;
        }
    }
}