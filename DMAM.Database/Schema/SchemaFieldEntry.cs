using System;

namespace DMAM.Database.Schema
{
    public abstract class SchemaFieldEntry
    {
        protected SchemaFieldEntry(string displayName, string dbColumnName, string metadataName)
        {
            DisplayName = displayName;
            DbColumnName = dbColumnName;
            MetadataName = metadataName;
        }

        public string DisplayName { get; private set; }
        public string DbColumnName { get; private set; }
        public string MetadataName { get; private set; }

        public abstract SchemaFieldValue CreateValue();
    }
}
