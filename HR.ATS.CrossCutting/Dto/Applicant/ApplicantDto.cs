using System;
using System.Collections.Generic;

namespace HR.ATS.CrossCutting.Dto.Applicant
{
    public class ExperienceDTO
    {
        public string Company { get; set; }
        public string Description { get; set; }
        public DateTime PeriodStartDate { get; set; }
        public DateTime? PeriodEndDate { get; set; }
    }

    public class ResumeDTO
    {
        public string Introduction { get; set; }
        public IEnumerable<ExperienceDTO> Experiences { get; set; }
    }
}