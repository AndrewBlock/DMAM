using System;
using System.Collections.Generic;

namespace DMAM.Core.Schema
{
    public abstract class SchemaBase : ISchema
    {
        public SchemaBase()
        {
            Schema = LoadSchema();
        }

        public IEnumerable<ISchemaFieldEntry> Schema { get; private set; }

        protected abstract string TableName { get; }
        protected abstract IEnumerable<ISchemaFieldEntry> LoadSchema();
    }
}
