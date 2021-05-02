using HR.ATS.Domain.Opening;
using HR.ATS.Infrastructure.Repository.Common;
using MongoDB.Driver;

namespace HR.ATS.Infrastructure.Repository
{
    public class OpeningRepository : GenericRepository<Opening>, IOpeningRepository
    {
        public OpeningRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}