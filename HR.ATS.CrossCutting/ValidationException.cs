using System;

namespace HR.ATS.CrossCutting
{
    public class ValidationException: Exception
    {
        public ValidationException(string? message) : base(message)
        {
        }
    }
    
    public class ValidationFieldRequiredException: ValidationException
    {
        public ValidationFieldRequiredException(string fieldName) : base($"The {fieldName} is required.")
        {
        }
    }
}