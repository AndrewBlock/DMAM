using System;

namespace DMAM.Database.Schema
{
    internal class InvalidTableSchemaException : Exception
    {
        public InvalidTableSchemaException()
            : base()
        {
        }

        public InvalidTableSchemaException(string message)
            : base(message)
        {
        }

        public InvalidTableSchemaException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
