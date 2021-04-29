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
    }
}