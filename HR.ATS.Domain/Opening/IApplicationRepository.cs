using System;
using System.Threading.Tasks;
using HR.ATS.Domain.Common;

namespace HR.ATS.Domain.Opening
{
    public interface IApplicationRepository : IRepository<Application>
    {
        public Task<bool> HasAlreadyApplied(Applicant.Applicant applicant, Opening opening);
        Task DeleteAllApplicationsFromOpening(Guid openingId);
    }
}