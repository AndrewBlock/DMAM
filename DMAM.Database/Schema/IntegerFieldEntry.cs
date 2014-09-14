using System;

namespace DMAM.Database.Schema
{
    public class IntegerFieldEntry : SchemaFieldEntry<int>
    {
        public IntegerFieldEntry(string columnName, string displayName, string metadataName, int minimumValue, int maximumValue)
            : base(columnName, displayName, metadataName)
        {
            MinimumValue = minimumValue;
            MinimumValue = maximumValue;
        }

        public int MinimumValue { get; private set; }
        public int MaximumValue { get; private set; }
    }
}
