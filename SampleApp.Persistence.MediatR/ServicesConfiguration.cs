using AutoMapper;
using MediatR;

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SampleApp.Persistence.MediatR
{
    public static class ServicesConfiguration
    {
        public static void AddMediatRContext(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(assembly);

            services.AddAutoMapper(typeof(ServicesConfiguration));
        }
    }
}