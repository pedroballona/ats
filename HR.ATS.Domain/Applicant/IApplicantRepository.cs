using System;
using System.Threading.Tasks;
using HR.ATS.Domain.Common;

namespace HR.ATS.Domain.Applicant
{
    public interface IApplicantRepository : IRepository<Applicant>
    {
        public Task<Applicant?> GetApplicantFromPerson(Guid personId);
    }
}