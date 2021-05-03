using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HR.ATS.CrossCutting.Dto.Applicant;
using HR.ATS.Domain.Applicant;
using MediatR;

namespace HR.ATS.Query.Applicant
{
    public class GetResumeFromApplicantQuery : IRequest<ApplicantDto?>
    {
        public GetResumeFromApplicantQuery(Guid applicantId)
        {
            ApplicantId = applicantId;
        }

        public Guid ApplicantId { get; }
    }

    internal class GetResumeFromApplicantQueryHandler : IRequestHandler<GetResumeFromApplicantQuery, ApplicantDto?>
    {
        private readonly IApplicantRepository _applicantRepository;

        public GetResumeFromApplicantQueryHandler(IApplicantRepository applicantRepository)
        {
            _applicantRepository = applicantRepository;
        }

        public async Task<ApplicantDto?> Handle(
            GetResumeFromApplicantQuery request,
            CancellationToken cancellationToken
        )
        {
            var applicant = await _applicantRepository.FindAsync(request.ApplicantId, cancellationToken);

            if (applicant is null) return null;

            var result = new ApplicantDto
            {
                Name = applicant.PersonReference.Name
            };

            if (applicant.Resume is not null)
                result.Resume = new ResumeDto
                {
                    Experiences = applicant.Resume.Experiences.Items.Select(
                        e => new ExperienceDto
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