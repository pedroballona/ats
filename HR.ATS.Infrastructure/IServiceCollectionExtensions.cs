using System;
using HR.ATS.Infrastructure.Mongo;
using HR.ATS.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace HR.ATS.Infrastructure
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            BsonSerializer.RegisterIdGenerator(typeof(Guid), CombGuidGenerator.Instance);
            services.AddHttpContextAccessor();
            services.AddScoped<IMongoDatabaseNameProvider, MongoDatabaseNameProvider>();
            services.AddScoped(MongoDatabaseFactory.CreateDatabase);

            // Repositories
            services.AddScoped<IPersonRepository, PersonRepository>();

            return services;
        }
    }
}