using System;

namespace DMAM.Core.Schema
{
    public class ForeignKeyFieldEntry : SchemaFieldEntry<object>
    {
        public ForeignKeyFieldEntry(string foreignColumnName, Type foreignSchemaType)
            : base(string.Empty, foreignColumnName, string.Empty)
        {
            ForeignSchemaType = foreignSchemaType;
        }

        public Type ForeignSchemaType { get; private set; }
    }
}
