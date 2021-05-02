using System.Threading.Tasks;
using HR.ATS.CrossCutting;

namespace HR.ATS.Domain.Opening
{
    public interface IOpeningApplicator
    {
        Task<Application> ApplyToOpening(Applicant.Applicant applicant, Opening opening);
    }

    public class OpeningApplicator : IOpeningApplicator
    {
        private readonly IApplicationRepository _applicationRepository;

        public OpeningApplicator(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<Application> ApplyToOpening(Applicant.Applicant applicant, Opening opening)
        {
            if (await _applicationRepository.HasAlreadyApplied(applicant, opening))
                throw new ValidationException("You already applied to this opening.");

            var application = opening.Apply(applicant);
            await _applicationRepository.CreateAsync(application);
            return application;
        }
    }
}