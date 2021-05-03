using System;
using HR.ATS.Domain.Common;

namespace HR.ATS.Domain.Opening.Events
{
    public class OpeningDeletedEvent : IDomainEvent
    {
        public OpeningDeletedEvent(Guid openingId)
        {
            OpeningId = openingId;
        }

        public Guid OpeningId { get; }
    }
}