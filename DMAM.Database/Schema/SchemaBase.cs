using System;
using System.Collections.Generic;

namespace DMAM.Database.Schema
{
    public abstract class SchemaBase : ISchema
    {
        public SchemaBase()
        {
            Schema = LoadSchema();
        }

        public IEnumerable<SchemaFieldEntry> Schema { get; private set; }

        protected abstract IEnumerable<SchemaFieldEntry> LoadSchema();
    }
}
