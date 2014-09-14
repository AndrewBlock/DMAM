using System;

namespace DMAM.Core.Schema
{
    public interface ISchemaFieldValue
    {
        ISchemaFieldEntry FieldEntry { get; }
        Type ValueType { get; }
        object Value { get; set; }
    }
}
