using System;
using System.Windows.Input;

using DMAM.Core.MVVM;

namespace DMAM.DataModels
{
    public class FieldValue : ViewModelBase, IDisposable
    {
        private readonly Command _revertCommand;
        private readonly Command _removeCommand;

        private string _value = string.Empty;
        private string _originalValue = string.Empty;
        private bool _isModified;
        private bool _isReadOnly;

        public FieldValue(string fieldName, string displayName, bool canRemove)
        {
            FieldName = fieldName;
            DisplayName = displayName;
            CanRemove = canRemove;

            _revertCommand = new Command(this, "IsModified", "Revert");
            _removeCommand = new Command(this, "CanRemove", "Remove");
        }

        public void Dispose()
        {
            _removeCommand.Dispose();
            _removeCommand.Dispose();
        }

        public event FieldValueHandler RemoveRequested;
        public event FieldValueHandler IsModifiedUpdated;

        public ICommand RevertCommand
        {
            get
            {
                return _revertCommand;
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                return _removeCommand;
            }
        }

        public string FieldName { get; private set; }
        public string DisplayName { get; private set; }
        public bool CanRemove { get; private set; }

        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }

                if (value == _value)
                {
                    return;
                }

                _value = value;
                NotifyPropertyChanged("Value");
                UpdateDisplayStates();
            }
        }

        public bool IsModified
        {
            get
            {
                return _isModified;
            }
            private set
            {
                if (value == _isModified)
                {
                    return;
                }

                _isModified = value;
                NotifyPropertyChanged("IsModified");

                if (IsModifiedUpdated != null)
                {
                    IsModifiedUpdated(this);
                }
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }
            private set
            {
                if (value == _isReadOnly)
                {
                    return;
                }

                _isReadOnly = value;
                NotifyPropertyChanged("IsReadOnly");
            }
        }

        public void Revert()
        {
            if (!IsModified && !IsReadOnly)
            {
                return;
            }
        }

        public void Remove()
        {
            if ((RemoveRequested == null) || (!CanRemove))
            {
                return;
            }

            RemoveRequested(this);
        }

        public void SetOriginalValue(string originalValue)
        {
            if (_originalValue == _value)
            {
                _originalValue = originalValue;
                Value = originalValue;
            }
            else
            {
                _originalValue = originalValue;
            }

            UpdateDisplayStates();
        }

        private void UpdateDisplayStates()
        {
            IsModified = (_value != _originalValue);
        }
    }
}
