using System;
using System.Collections.Generic;

namespace HR.ATS.CrossCutting.Dto.Applicant
{
    public class ExperienceDto
    {
        public string? Company { get; set; }
        public string? Description { get; set; }
        public DateTime PeriodStartDate { get; set; }
        public DateTime? PeriodEndDate { get; set; }
    }

    public class ResumeDto
    {
        public string? Introduction { get; set; }
        public IEnumerable<ExperienceDto>? Experiences { get; set; }
    }

    public class SimpleApplicantDto
    {
        public string? Name { get; set; }
        public Guid Id { get; set; }
    }

    public class ApplicantDto
    {
        public string? Name { get; set; }
        public Guid Id { get; set; }
        public ResumeDto? Resume { get; set; }
    }
}