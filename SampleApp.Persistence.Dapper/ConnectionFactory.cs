using System.Data;
using System.Data.Common;

namespace SampleApp.Persistence.Dapper
{
    public class ConnectionFactory :IConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection GetConnection
        {
            get
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                IDbConnection conn = factory.CreateConnection();
                conn.ConnectionString = _connectionString;
                conn.Open();

                return conn;
            }
        }
    }
}