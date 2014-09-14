using System;
using System.Collections.Generic;

namespace DMAM.Database.Schema
{
    public abstract class TableSchemaBase : ITableSchema
    {
        public TableSchemaBase()
        {
            Schema = LoadSchema();
        }

        public abstract string TableName { get; }
        public IEnumerable<ISchemaFieldEntry> Schema { get; private set; }

        protected abstract IEnumerable<ISchemaFieldEntry> LoadSchema();
    }
}
