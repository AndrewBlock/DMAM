using System;

namespace DMAM.Database.Schema
{
    public class PrimaryKeyFieldEntry : SchemaFieldEntry<object>
    {
        public PrimaryKeyFieldEntry(string columnName)
            : base(string.Empty, columnName, string.Empty)
        {
        }
    }
}
