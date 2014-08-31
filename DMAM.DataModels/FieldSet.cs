using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using DMAM.Core.MVVM;

namespace DMAM.DataModels
{
    public class FieldSet : ViewModelBase, IDisposable
    {
        public ObservableCollection<FieldValue> _fieldValues = new ObservableCollection<FieldValue>();

        public FieldSet()
        {
        }

        public void Dispose()
        {
            foreach (var fieldValue in _fieldValues)
            {
                DetachFieldValue(fieldValue);
                fieldValue.Dispose();
            }

            _fieldValues.Clear();
        }

        public IEnumerable<FieldValue> FieldValues
        {
            get
            {
                return _fieldValues;
            }
        }

        private void AttachFieldValue(FieldValue fieldValue)
        {
            fieldValue.IsModifiedUpdated += fieldValue_IsModifiedUpdated;
            fieldValue.RemoveRequested += fieldValue_RemoveRequested;
        }

        private void DetachFieldValue(FieldValue fieldValue)
        {
            fieldValue.RemoveRequested -= fieldValue_RemoveRequested;
            fieldValue.IsModifiedUpdated -= fieldValue_IsModifiedUpdated;
        }

        private void fieldValue_IsModifiedUpdated(FieldValue fieldValue)
        {
        }

        private void fieldValue_RemoveRequested(FieldValue fieldValue)
        {
        }
    }
}
