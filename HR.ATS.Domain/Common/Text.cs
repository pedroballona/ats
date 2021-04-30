using System;
using System.Collections.Generic;

namespace HR.ATS.Domain.Common
{
    public class Text : ValueObject
    {
        public Text(string value)
        {
            Value = CheckValue(value);
        }

        public string Value { get; set; }

        public int Length => Value.Length;

        private static string CheckValue(string? value)
        {
            value = value?.Trim();

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(nameof(value));

            return value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(Text name)
        {
            return name.Value;
        }

        public static implicit operator Text(string value)
        {
            return new(value);
        }
    }
}