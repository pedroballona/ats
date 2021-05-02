using HR.ATS.Domain.Opening;
using Microsoft.Extensions.DependencyInjection;

namespace HR.ATS.Domain
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IOpeningApplicator, OpeningApplicator>();
            return services;
        }
    }
}