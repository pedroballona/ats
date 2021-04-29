using Microsoft.AspNetCore.Http;

namespace HR.ATS.Infrastructure.Mongo
{
    internal interface IMongoDatabaseNameProvider
    {
        string DatabaseName { get; }
    }

    internal class MongoDatabaseNameProvider : IMongoDatabaseNameProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MongoDatabaseNameProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string DatabaseName => $"ats-{_httpContextAccessor.HttpContext.GetTenantName()}-db";
    }
}