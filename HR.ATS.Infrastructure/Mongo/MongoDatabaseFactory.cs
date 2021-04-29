using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace HR.ATS.Infrastructure.Mongo
{
    internal static class MongoDatabaseFactory
    {
        public static IMongoDatabase CreateDatabase(IServiceProvider provider)
        {
            var nameProvider = provider.GetRequiredService<IMongoDatabaseNameProvider>();
            var configurations = provider.GetRequiredService<IConfiguration>();
            var mongoUrl = configurations.GetConnectionString("mongodb");
            var client = new MongoClient(mongoUrl);
            return client.GetDatabase(nameProvider.DatabaseName);
        }
    }
}