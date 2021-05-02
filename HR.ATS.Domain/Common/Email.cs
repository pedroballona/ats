using System.Collections.Generic;
using System.Net.Mail;
using HR.ATS.CrossCutting;

namespace HR.ATS.Domain.Common
{
    public class Email : ValueObject
    {
        public Email(string value)
        {
            Value = CheckValue(value);
        }

        public string Value { get; private set; }

        private static string CheckValue(string? value)
        {
            value = value?.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(value) || !IsValidEmail(value))
                throw new ValidationFieldRequiredException($"The provided value '{value}' is not a valid email");

            return value;
        }

        private static bool IsValidEmail(string value)
        {
            try
            {
                var addr = new MailAddress(value);
                return addr.Address == value;
            }
            catch
            {
                return false;
            }
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }

        public static implicit operator Email(string value)
        {
            return new(value);
        }
    }
}