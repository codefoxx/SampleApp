using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Data;

namespace SampleApp.Persistence.Dapper
{
    public static class ServicesConfiguration
    {
        public static void AddSampleAppContext(this IServiceCollection services, Action<SampleAppContextOptions> action)
        {
            var config = new SampleAppContextOptions();
            action?.Invoke(config);

            services.AddTransient<IDbConnection>((sp) => new SqlConnection(config.ConnectionString));
        }
    }

    public class SampleAppContextOptions
    {
        public string ConnectionString { get; set; }
    }
}