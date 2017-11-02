using System;
using System.Runtime.Serialization;

namespace UserStorageServices.Exceptions
{
    public class AgeExceedsLimitsException : Exception
    {
        public AgeExceedsLimitsException()
        {
        }

        public AgeExceedsLimitsException(string message) : base(message)
        {
        }

        public AgeExceedsLimitsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AgeExceedsLimitsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
