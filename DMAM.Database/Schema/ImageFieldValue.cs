using System;

namespace DMAM.Database.Schema
{
    public class ImageFieldValue : SchemaFieldValue
    {
        private Guid _id = Guid.NewGuid();

        public ImageFieldValue(ImageFieldEntry fieldEntry)
            : base(fieldEntry)
        {
        }

        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value == _id)
                {
                    return;
                }

                _id = value;
                NotifyPropertyChanged("Id");
            }
        }
    }
}
