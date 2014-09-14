using System;

namespace DMAM.Database.Schema
{
    public class TextFieldEntry : SchemaFieldEntry<string>
    {
        public TextFieldEntry(string columnName, string displayName, string metadataName, int maximumLength)
            : base(columnName, displayName, metadataName)
        {
            MaximumLength = maximumLength;
        }

        public int MaximumLength { get; private set; }
    }
}
