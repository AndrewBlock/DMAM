using System.Collections.Generic;

namespace DMAM.Core.Schema
{
    public interface ISchema
    {
        IEnumerable<SchemaFieldEntry> Schema { get; }
    }
}
