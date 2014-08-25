using System;

namespace DMAM.Database.Schema
{
    public abstract class RelationalFieldEntry : SchemaFieldEntry
    {
        protected RelationalFieldEntry(string displayName, string metadataName, string dbColumnName, string directory, string extension)
            : base(displayName, dbColumnName, metadataName)
        {
            Directory = directory;
            Extension = extension;
        }

        public string Directory { get; private set; }
        public string Extension { get; private set; }
    }
}
