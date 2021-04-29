using System;
using HR.ATS.Domain.Common;

namespace HR.ATS.Domain.Person
{
    public class Person : Entity
    {
        private Person()
        {
        }

        public Person(
            Name name,
            Email email,
            UserId userId
        )
        {
            Name = CheckName(name);
            Email = CheckEmail(email);
            UserId = CheckUserId(userId);
        }

        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public UserId UserId { get; private set; }

        private static UserId CheckUserId(UserId userId)
        {
            return userId ?? throw new ArgumentNullException(nameof(userId));
        }

        private static Email CheckEmail(Email email)
        {
            return email ?? throw new ArgumentNullException(nameof(email));
        }

        private static Name CheckName(Name name)
        {
            return name ?? throw new ArgumentNullException(nameof(name));
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Email)}: {Email}, {nameof(UserId)}: {UserId}";
        }
    }
}