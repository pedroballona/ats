using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HR.ATS.Command
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommand(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceCollectionExtensions));

            return services;
        }
    }
}