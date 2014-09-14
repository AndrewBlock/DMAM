using System;

namespace DMAM.Core.Schema
{
    public class PrimaryKeyFieldEntry : SchemaFieldEntry<object>
    {
        public PrimaryKeyFieldEntry(string columnName)
            : base(string.Empty, columnName, string.Empty)
        {
        }
    }
}
