using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HR.ATS.CrossCutting;
using HR.ATS.CrossCutting.Dto.Applicant;
using HR.ATS.Domain.Applicant;
using HR.ATS.Domain.Common;
using HR.ATS.Domain.Person;
using MediatR;

namespace HR.ATS.Command.Applicant
{
    public class UpdateResumeForLoggedApplicantCommand : IRequest<ResumeDto>
    {
        public UpdateResumeForLoggedApplicantCommand(ResumeDto resume)
        {
            Resume = resume;
        }

        public ResumeDto Resume { get; }
    }

    internal class
        UpdateResumeForLoggedApplicantCommandHandler : IRequestHandler<UpdateResumeForLoggedApplicantCommand, ResumeDto>
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly IPersonRepository _personRepository;

        public UpdateResumeForLoggedApplicantCommandHandler(
            IPersonRepository personRepository,
            IApplicantRepository applicantRepository
        )
        {
            _personRepository = personRepository;
            _applicantRepository = applicantRepository;
        }

        public async Task<ResumeDto> Handle(
            UpdateResumeForLoggedApplicantCommand request,
            CancellationToken cancellationToken
        )
        {
            var loggedPerson = await _personRepository.GetLoggedPerson();

            if (loggedPerson is null) throw new ValidationException("The current logged user is not a person.");

            var applicant = await _applicantRepository.GetApplicantFromPerson(loggedPerson.Id);
            Resume resume = new(request.Resume.Introduction!, new Experiences(
                request.Resume.Experiences?.Select(
                    e => new Experience(
                        e.Company!,
                        e.Description!,
                        new OpenEndedPeriod(e.PeriodStartDate, e.PeriodEndDate)
                    )
                )!
            ));
            if (applicant is null)
            {
                applicant = new Domain.Applicant.Applicant(loggedPerson, resume);
                await _applicantRepository.CreateAsync(applicant, cancellationToken);
            }
            else
            {
                applicant.ChangeResume(resume);
                await _applicantRepository.UpdateAsync(applicant, cancellationToken);
            }

            return request.Resume;
        }
    }
}