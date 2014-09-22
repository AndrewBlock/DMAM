using System;

namespace DMAM.Core.Metadata
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExternalFieldAttribute : Attribute
    {
        public ExternalFieldAttribute(string fieldName)
        {
            FieldName = fieldName;
        }

        public string FieldName { get; private set; }
    }
}
