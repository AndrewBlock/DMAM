using System;

using DMAM.Core.MVVM;

namespace DMAM.Core.Schema
{
    public class SchemaFieldValue<T> : ViewModelBase, ISchemaFieldValue
    {
        private T _value = default(T);

        internal SchemaFieldValue(SchemaFieldEntry<T> fieldEntry)
        {
            FieldEntry = fieldEntry;
        }

        public ISchemaFieldEntry FieldEntry { get; private set; }

        public Type ValueType
        {
            get
            {
                return FieldEntry.ValueType;
            }
        }

        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!typeof(T).IsInstanceOfType(value))
                {
                    throw new InvalidSchemaFieldValueException();
                }

                if (object.Equals(value, _value))
                {
                    return;
                }

                _value = (T) value;
                NotifyPropertyChanged("Value");
            }
        }
    }
}
