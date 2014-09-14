using System;

using DMAM.Core.MVVM;

namespace DMAM.Core.Schema
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
