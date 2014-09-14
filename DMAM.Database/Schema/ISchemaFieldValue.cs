using System;

namespace DMAM.Database.Schema
{
    public interface ISchemaFieldValue
    {
        ISchemaFieldEntry FieldEntry { get; }
        Type ValueType { get; }
        object Value { get; set; }
    }
}
