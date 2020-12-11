using SampleApp.Core.Domain;
using System.Threading.Tasks;

namespace SampleApp.Core.Repositories
{
    public interface IAuthorRepository : IRepository<Author, int>
    {
        Task<Author> GetAuthorWithCourses(int id);
    }
}