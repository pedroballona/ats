using System;
using System.Collections.Generic;

namespace HR.ATS.Domain.Common
{
    public class UserId : ValueObject
    {
        public UserId(long value)
        {
            Value = CheckValue(value);
        }

        public long Value { get; private set; }

        private static long CheckValue(long value)
        {
            if (value <= 0)
            {
                throw new ArgumentException(nameof(value));
            }

            return value;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator long(UserId name) => name.Value;
        public static implicit operator UserId(long value) => new(value);
    }
}