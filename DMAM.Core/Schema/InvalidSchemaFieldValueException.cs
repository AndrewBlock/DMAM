using System;

namespace DMAM.Core.Schema
{
    internal class InvalidSchemaFieldValueException : Exception
    {
        public InvalidSchemaFieldValueException()
            : base()
        {
        }

        public InvalidSchemaFieldValueException(string message)
            : base(message)
        {
        }

        public InvalidSchemaFieldValueException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
