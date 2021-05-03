using HR.ATS.Domain.Opening;
using Microsoft.Extensions.DependencyInjection;

namespace HR.ATS.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IOpeningApplicator, OpeningApplicator>();
            return services;
        }
    }
}