using System;

namespace DMAM.Gracenote.Responses
{
    internal class InvalidServerResponseException : Exception
    {
        public InvalidServerResponseException()
            : base()
        {
        }

        public InvalidServerResponseException(string message)
            : base(message)
        {
        }

        public InvalidServerResponseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
