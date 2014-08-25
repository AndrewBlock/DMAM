using System;

namespace DMAM.Database.Schema
{
    public class TextFieldEntry : SchemaFieldEntry
    {
        public TextFieldEntry(string displayName, string dbColumnName, string metadataName, int maximumLength)
            : base(displayName, dbColumnName, metadataName)
        {
            MaximumLength = maximumLength;
        }

        public int MaximumLength { get; private set; }

        public override SchemaFieldValue CreateValue()
        {
            return new TextFieldValue(this);
        }
    }
}
