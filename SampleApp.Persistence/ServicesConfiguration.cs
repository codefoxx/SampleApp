using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Core.Repositories;
using SampleApp.Persistence.Repositories;
using SampleApp.Persistence.Repositories.Cached;
using StackExchange.Redis;

using System;

namespace SampleApp.Persistence
{
    public static class ServicesConfiguration
    {
        public static void AddSampleAppContext(this IServiceCollection services, Action<SampleAppContextOptions> action)
        {
            var config = new SampleAppContextOptions();
            action?.Invoke(config);

            services.AddDbContextFactory<ApplicationDbContext>(options => options.UseSqlServer(config.SqlConnectionString));

            services.AddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect(config.RedisConnectionString));

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.Decorate<IAuthorRepository, CachedAuthorRepository>();

        }

        public static void AddSampleAppMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ServicesConfiguration));
        }
    }

    public class SampleAppContextOptions
    {
        public string SqlConnectionString { get; set; }
        public string RedisConnectionString { get; set; }
    }
}