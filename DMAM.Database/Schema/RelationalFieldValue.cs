using System;
using System.IO;

namespace DMAM.Database.Schema
{
    public abstract class RelationalFieldValue : SchemaFieldValue
    {
        private readonly RelationalFieldEntry _relationalFieldEntry;

        private Guid _id = Guid.NewGuid();
        private string _fileName;
        private string _relativePath;

        private Stream _stream;

        public RelationalFieldValue(RelationalFieldEntry fieldEntry)
            : base(fieldEntry)
        {
            _relationalFieldEntry = fieldEntry;
            UpdateIdFields();
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
                UpdateIdFields();

                NotifyPropertyChanged("Id");
                NotifyPropertyChanged("FileName");
                NotifyPropertyChanged("RelativePath");
            }
        }

        public string FileName
        {
            get
            {
                return _fileName;
            }
        }

        public string RelativePath
        {
            get
            {
                return _relativePath;
            }
        }

        protected Stream Data
        {
            get
            {
                return _stream;
            }
        }

        private void UpdateIdFields()
        {
            _fileName = Id.ToString() + _relationalFieldEntry.Extension;
            _relativePath = Path.Combine(_relationalFieldEntry.Directory, FileName);
        }
    }
}
