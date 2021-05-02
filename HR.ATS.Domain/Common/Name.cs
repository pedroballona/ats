using System.Collections.Generic;
using HR.ATS.CrossCutting;

namespace HR.ATS.Domain.Common
{
    public class Name : ValueObject
    {
        public Name(string value)
        {
            Value = CheckName(value);
        }

        public string Value { get; private set; }

        private static string CheckName(string? value)
        {
            value = value?.Trim();
            if (string.IsNullOrWhiteSpace(value)) throw new ValidationFieldRequiredException("name");
            return value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(Name name)
        {
            return name.Value;
        }

        public static implicit operator Name(string value)
        {
            return new(value);
        }
    }
}