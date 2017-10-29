using System;
using System.Runtime.Serialization;

namespace UserStorageServices.Exceptions
{
    public class FirstNameIsNullOrEmptyException : Exception
    {
        public FirstNameIsNullOrEmptyException()
        {
        }

        public FirstNameIsNullOrEmptyException(string message) : base(message)
        {
        }

        public FirstNameIsNullOrEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FirstNameIsNullOrEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
