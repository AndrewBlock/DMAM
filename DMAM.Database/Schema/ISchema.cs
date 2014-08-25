using System.Collections.Generic;

namespace DMAM.Database.Schema
{
    public interface ISchema
    {
        IEnumerable<SchemaFieldEntry> Schema { get; }
    }
}
