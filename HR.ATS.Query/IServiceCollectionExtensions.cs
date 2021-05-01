using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HR.ATS.Query
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddQuery(this IServiceCollection services)
        {
            services.AddMediatR(typeof(IServiceCollectionExtensions));

            return services;
        }
    }
}