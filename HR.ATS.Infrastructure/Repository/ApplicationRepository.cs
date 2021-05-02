using System.Threading.Tasks;
using HR.ATS.Domain.Applicant;
using HR.ATS.Domain.Opening;
using HR.ATS.Infrastructure.Repository.Common;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HR.ATS.Infrastructure.Repository
{
    public class ApplicationRepository : GenericRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(IMongoDatabase database) : base(database)
        {
        }

        public async Task<bool> HasAlreadyApplied(Applicant applicant, Opening opening)
        {
            var hasAny = await Collection.AsQueryable()
                                         .Where(a => a.ApplicantId == applicant.Id && a.OpeningId == opening.Id)
                                         .AnyAsync();
            return hasAny;
        }
    }
}