using System;

namespace KTranslate.Infrastructure.Exceptions
{
    public class OptionalFeatureAccessException : Exception
    {
        public OptionalFeatureAccessException(string message) : base(message)
        {
            
        }

        public OptionalFeatureAccessException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
