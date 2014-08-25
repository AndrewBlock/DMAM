using System;

namespace DMAM.Database.Schema
{
    public class ImageFieldEntry : RelationalFieldEntry
    {
        public ImageFieldEntry(string displayName, string dbColumnName, string metadataName, string directory, string extension)
            : base(displayName, dbColumnName, metadataName, directory, extension)
        {
        }

        public override SchemaFieldValue CreateValue()
        {
            return new ImageFieldValue(this);
        }
    }
}
