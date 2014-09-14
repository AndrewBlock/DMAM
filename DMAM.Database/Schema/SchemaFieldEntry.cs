using System;

namespace DMAM.Database.Schema
{
    public abstract class SchemaFieldEntry<T> : ISchemaFieldEntry
    {
        protected SchemaFieldEntry(string columnName, string displayName, string metadataName)
        {
            ColumnName = columnName;
            DisplayName = displayName;
            MetadataName = metadataName;
        }

        public string ColumnName { get; private set; }
        public string DisplayName { get; private set; }
        public string MetadataName { get; private set; }

        public Type ValueType
        {
            get { return typeof(T); }
        }

        public ISchemaFieldValue CreateValue()
        {
            return new SchemaFieldValue<T>(this);
        }
    }
}
