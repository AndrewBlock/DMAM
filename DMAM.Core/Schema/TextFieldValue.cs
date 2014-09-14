using System;

namespace DMAM.Core.Schema
{
    public class TextFieldValue : SchemaFieldValue
    {
        private string _value = string.Empty;

        public TextFieldValue(TextFieldEntry fieldEntry)
            : base(fieldEntry)
        {
        }

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value == _value)
                {
                    return;
                }

                _value = value;
                NotifyPropertyChanged("Value");
            }
        }
    }
}
