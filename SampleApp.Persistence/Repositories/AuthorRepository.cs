using Microsoft.EntityFrameworkCore;

using SampleApp.Core;
using SampleApp.Core.Domain;
using SampleApp.Core.Repositories;

using System.Threading.Tasks;

namespace SampleApp.Persistence.Repositories
{
    public class AuthorRepository : Repository<Author, int>, IAuthorRepository
    {
        public AuthorRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }


        public async Task<Author> GetAuthorWithCourses(int id)
        {
            return await UnitOfWork.Query<Author>()
                .Include(author => author.Courses)
                .SingleOrDefaultAsync(author => author.Id.Equals(id));
        }
    }
}