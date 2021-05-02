using System;
using HR.ATS.CrossCutting;
using HR.ATS.Domain.Common;

namespace HR.ATS.Domain.Opening
{
    public class Application : Entity
    {
        public Application(Guid applicantId, Guid openingId)
        {
            ApplicantId = applicantId == default
                              ? throw new ValidationFieldRequiredException(nameof(applicantId))
                              : applicantId;
            OpeningId = openingId == default
                            ? throw new ValidationFieldRequiredException(nameof(openingId))
                            : openingId;
        }

        public Guid ApplicantId { get; private set; }
        public Guid OpeningId { get; private set; }
    }
}