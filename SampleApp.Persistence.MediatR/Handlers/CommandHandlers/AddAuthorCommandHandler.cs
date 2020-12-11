using MediatR;

using Microsoft.EntityFrameworkCore;

using SampleApp.Core.Domain;
using SampleApp.Persistence.MediatR.Commands;
using SampleApp.Persistence.Repositories;

using System.Threading;
using System.Threading.Tasks;

namespace SampleApp.Persistence.MediatR.Handlers.CommandHandlers
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Author>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;


        public AddAuthorCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }


        public async Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            await using var dbContext = _contextFactory.CreateDbContext();

            var unitOfWork = new UnitOfWork(dbContext);
            var authorRepository = new AuthorRepository(unitOfWork);
            var author = new Author()
            {
                Name = request.Name,
                Courses = request.Courses
            };

            authorRepository.Add(author);
            await unitOfWork.CommitAsync(cancellationToken);

            return author;
        }
    }
}