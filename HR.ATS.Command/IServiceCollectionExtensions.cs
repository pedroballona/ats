using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HR.ATS.Command
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCommand(this IServiceCollection services)
        {
            services.AddMediatR(typeof(IServiceCollectionExtensions));

            return services;
        }
    }
}