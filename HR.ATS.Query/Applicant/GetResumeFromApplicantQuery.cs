using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HR.ATS.CrossCutting.Dto.Applicant;
using HR.ATS.Domain.Applicant;
using MediatR;

namespace HR.ATS.Query.Applicant
{
    public class GetResumeFromApplicantQuery : IRequest<ApplicantDTO?>
    {
        public GetResumeFromApplicantQuery(Guid applicantId)
        {
            ApplicantId = applicantId;
        }

        public Guid ApplicantId { get; }
    }

    internal class GetResumeFromApplicantQueryHandler : IRequestHandler<GetResumeFromApplicantQuery, ApplicantDTO?>
    {
        private readonly IApplicantRepository _applicantRepository;

        public GetResumeFromApplicantQueryHandler(IApplicantRepository applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }

        public async Task<ApplicantDTO?> Handle(
            GetResumeFromApplicantQuery request,
            CancellationToken cancellationToken
        )
        {
            var applicant = await _applicantRepository.FindAsync(request.ApplicantId, cancellationToken);

            if (applicant is null) return null;

            var result = new ApplicantDTO
            {
                Name = applicant.PersonReference.Name
            };

            if (applicant.Resume is not null)
                result.Resume = new ResumeDTO
                {
                    Experiences = applicant.Resume.Experiences.Items.Select(
                        e => new ExperienceDTO
                        {
                            Company = e.Company,
                            Description = e.Description,
                            PeriodStartDate = e.Period.StartDate,
                            PeriodEndDate = e.Period.EndDate
                        }
                    ),
                    Introduction = applicant.Resume.Introduction
                };
            return result;
        }
    }
}