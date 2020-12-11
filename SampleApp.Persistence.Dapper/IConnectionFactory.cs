using System.Data;

namespace SampleApp.Persistence.Dapper
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }
    }
}