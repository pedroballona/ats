using System;
using System.Threading.Tasks;
using HR.ATS.Domain.Applicant;
using HR.ATS.Infrastructure.Repository.Common;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HR.ATS.Infrastructure.Repository
{
    internal class ApplicantRepository : GenericRepository<Applicant>, IApplicantRepository
    {
        public ApplicantRepository(IMongoDatabase database) : base(database)
        {
        }

        public async Task<Applicant?> GetApplicantFromPerson(Guid personId)
        {
            var result = await Collection.AsQueryable()
                                         .Where(p => p.PersonReference.Id == personId)
                                         .FirstOrDefaultAsync();
            return result;
        }
    }
}