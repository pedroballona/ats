using System;
using System.Threading;
using System.Threading.Tasks;
using HR.ATS.CrossCutting;
using HR.ATS.CrossCutting.Dto.Opening;
using HR.ATS.Domain.Applicant;
using HR.ATS.Domain.Opening;
using HR.ATS.Domain.Person;
using MediatR;

namespace HR.ATS.Command.Applicant
{
    public class ApplyLoggedApplicantToOpeningCommand : IRequest<ApplicationDto>
    {
        public ApplyLoggedApplicantToOpeningCommand(Guid openingId)
        {
            OpeningId = openingId;
        }

        public Guid OpeningId { get; }
    }

    internal class
        ApplyLoggedApplicantToOpeningCommandHandler : IRequestHandler<ApplyLoggedApplicantToOpeningCommand,
            ApplicationDto>
    {
        private readonly IApplicantRepository _applicantRepository;
        private readonly IOpeningApplicator _openingApplicator;
        private readonly IOpeningRepository _openingRepository;
        private readonly IPersonRepository _personRepository;

        public ApplyLoggedApplicantToOpeningCommandHandler(
            IPersonRepository personRepository,
            IApplicantRepository applicantRepository,
            IOpeningRepository openingRepository,
            IOpeningApplicator openingApplicator
        )
        {
            _personRepository = personRepository;
            _applicantRepository = applicantRepository;
            _openingRepository = openingRepository;
            _openingApplicator = openingApplicator;
        }

        public async Task<ApplicationDto> Handle(
            ApplyLoggedApplicantToOpeningCommand request,
            CancellationToken cancellationToken
        )
        {
            var loggedPerson = await _personRepository.GetLoggedPerson();
            var applicant = loggedPerson is null
                                ? null
                                : await _applicantRepository.GetApplicantFromPerson(loggedPerson.Id);
            if (applicant is null)
                throw new ValidationException("You must create a resume before applying to an opening.");

            var opening = await _openingRepository.FindAsync(request.OpeningId, cancellationToken);
            if (opening is null) throw new ValidationException("The selected opening doesn't exist.");

            var application = await _openingApplicator.ApplyToOpening(applicant, opening);

            return new ApplicationDto
            {
                ApplicantId = application.ApplicantId,
                OpeningId = application.OpeningId
            };
        }
    }
}