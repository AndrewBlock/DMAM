using System;

namespace DMAM.Database.Schema
{
    public class AudioFieldEntry : RelationalFieldEntry
    {
        public AudioFieldEntry(string displayName, string dbColumnName, string metadataName, string directory, string extension)
            : base(displayName, dbColumnName, metadataName, directory, extension)
        {
        }

        public override SchemaFieldValue CreateValue()
        {
            return new AudioFieldValue(this);
        }
    }
}
