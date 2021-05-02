using System;
using HR.ATS.Domain.Common;

namespace HR.ATS.Domain.Opening.Events
{
    public class OpeningDeletedEvent: IDomainEvent
    {
        public Guid OpeningId { get; }

        public OpeningDeletedEvent(Guid openingId)
        {
            OpeningId = openingId;
        }
    }
}