using System;

namespace DMAM.Core.Schema
{
    public interface ISchemaFieldEntry
    {
        string ColumnName { get; }
        string DisplayName { get; }
        string MetadataName { get; }
        Type ValueType { get; }

        ISchemaFieldValue CreateValue();
    }
}
