using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

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
