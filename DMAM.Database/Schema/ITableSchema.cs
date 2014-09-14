using System.Collections.Generic;

namespace DMAM.Database.Schema
{
    public interface ITableSchema
    {
        string TableName { get; }
        IEnumerable<ISchemaFieldEntry> Schema { get; }
    }
}
