using System;
using HR.ATS.Domain.Common;

namespace HR.ATS.Domain.Candidate
{
    public class Candidate : Entity
    {
        public Candidate(Name name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public Name Name { get; private set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}";
        }
    }
}