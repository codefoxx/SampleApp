using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SampleApp.Core;

using System;

namespace SampleApp.Persistence
{
    public static class ServicesConfiguration
    {
        public static void AddSampleAppContext(this IServiceCollection services, Action<SampleAppContextOptions> action)
        {
            var config = new SampleAppContextOptions();
            action?.Invoke(config);

            services.AddDbContext<SampleAppContext>(options => { options.UseSqlServer(config.ConnectionString); });

            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }

    public class SampleAppContextOptions
    {
        public string ConnectionString { get; set; }
    }
}