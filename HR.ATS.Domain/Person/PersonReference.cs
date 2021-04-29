using System;
using HR.ATS.Domain.Common;

namespace HR.ATS.Domain.Person
{
    public class PersonReference : EntityReference<Person>
    {
        public PersonReference(Guid id, Name name) : base(id)
        {
            Name = name;
        }

        public Name Name { get; private set; }
    }
}