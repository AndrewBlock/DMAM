using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using DMAM.Core.Collections;
using DMAM.Core.MVVM;

namespace DMAM.Core.DataModels
{
    public class FieldSet : ViewModelBase, IDisposable
    {
        private readonly IFieldValueCompare _comparer;

        private ObservableCollection<FieldValue> _fieldValues = new ObservableCollection<FieldValue>();

        public FieldSet(IFieldValueCompare comparer)
        {
            _comparer = comparer;
        }

        public void Dispose()
        {
            Clear();
        }

        public IEnumerable<FieldValue> FieldValues
        {
            get
            {
                return _fieldValues;
            }
        }

        public void Initialize(IEnumerable<FieldValue> fieldValues)
        {
            _fieldValues.InitializeSorted(fieldValues, _comparer);
            AttachFieldValues(fieldValues);
        }

        public void Clear()
        {
            DetachFieldValues();
            _fieldValues.Clear();
        }

        public void Add(FieldValue fieldValue)
        {
            _fieldValues.InsertSorted(fieldValue, _comparer);
            AttachFieldValue(fieldValue);
        }

        public void Remove(FieldValue fieldValue)
        {
            DetachFieldValue(fieldValue);
            _fieldValues.Remove(fieldValue);
        }

        private void AttachFieldValues(IEnumerable<FieldValue> fieldValues)
        {
            foreach (var fieldValue in fieldValues)
            {
                AttachFieldValue(fieldValue);
            }
        }

        private void DetachFieldValues()
        {
            foreach (var fieldValue in _fieldValues)
            {
                DetachFieldValue(fieldValue);
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
