using Microsoft.EntityFrameworkCore;

namespace SampleApp.Persistence
{
    public interface IDbContextFactory
    {
        ApplicationDbContext GetApplicationContext();
    }

    public sealed class DbContextFactory : IDbContextFactory
    {
        private readonly string _connectionString;

        public DbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ApplicationDbContext GetApplicationContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(_connectionString);

            return new ApplicationDbContext(builder.Options);
        }
    }
}