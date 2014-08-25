using System;

namespace DMAM.Gracenote.Responses
{
    internal class ServerReportedErrorException : Exception
    {
        public ServerReportedErrorException()
            : base()
        {
        }

        public ServerReportedErrorException(string message)
            : base(message)
        {
        }

        public ServerReportedErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
