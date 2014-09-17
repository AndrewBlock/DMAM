using System;

namespace DMAM.Database.Schema
{
    public class PrimaryKeyFieldEntry : SchemaFieldEntry<object>
    {
        public PrimaryKeyFieldEntry(string columnName)
            : base(columnName, string.Empty, string.Empty)
        {
        }
    }
}
