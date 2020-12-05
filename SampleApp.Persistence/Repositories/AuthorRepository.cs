using Microsoft.EntityFrameworkCore;

using SampleApp.Core.Domain;
using SampleApp.Core.Repositories;

using System.Linq;

namespace SampleApp.Persistence.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(SampleAppContext context) : base(context)
        {
        }

        public SampleAppContext SampleAppContext => Context as SampleAppContext;

        public Author GetAuthorWithCourses(int id)
        {
            return SampleAppContext.Authors
                .Include(a => a.Courses)
                .SingleOrDefault(a => a.Id == id);
        }
    }
}