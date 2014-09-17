using System;

namespace DMAM.Database.Schema
{
    public class ForeignKeyFieldEntry : SchemaFieldEntry<object>
    {
        public ForeignKeyFieldEntry(string foreignColumnName, Type foreignSchemaType)
            : base(foreignColumnName, string.Empty, string.Empty)
        {
            ForeignSchemaType = foreignSchemaType;
        }

        public Type ForeignSchemaType { get; private set; }
    }
}
