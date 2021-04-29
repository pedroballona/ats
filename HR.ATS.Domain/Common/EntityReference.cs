using System;
using System.Collections.Generic;

namespace HR.ATS.Domain.Common
{
    public abstract class EntityReference<T> : ValueObject where T : Entity
    {
        protected EntityReference(Guid id)
        {
            Id = CheckId(id);
        }

        public Guid Id { get; private set; }

        private Guid CheckId(Guid id)
        {
            if (id == default) throw new ArgumentNullException(nameof(id));

            return id;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}