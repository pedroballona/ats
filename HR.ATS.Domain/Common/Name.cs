using System;
using System.Collections.Generic;

namespace HR.ATS.Domain.Common
{
    public class Name : ValueObject
    {
        public Name(string value)
        {
            Value = CheckName(value);
        }

        public string Value { get; private set; }

        private static string CheckName(string value)
        {
            value = value?.Trim();
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
            return value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(Name name) => name.Value;
        public static implicit operator Name(string value) => new(value);
    }
}