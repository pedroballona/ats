using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HR.ATS.Query
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQuery(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions));

            return services;
        }
    }
}