using System;

using DMAM.Core.MVVM;

namespace DMAM.Database.Schema
{
    public abstract class SchemaFieldValue : ViewModelBase
    {
        protected SchemaFieldValue(SchemaFieldEntry fieldEntry)
        {
            FieldEntry = fieldEntry;
        }

        public SchemaFieldEntry FieldEntry { get; private set; }
    }
}
