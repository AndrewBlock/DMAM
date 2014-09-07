using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using DMAM.Album.Data.Models;

namespace DMAM.Album.Editors
{
    public partial class CoverArtEditor : IDisposable
    {
        private CoverArtData _coverArtData;

        public CoverArtEditor()
        {
            InitializeComponent();
        }

        public void Dispose()
        {
        }

        private void FieldValueEditor_DataContextChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            AttachDataContext();
        }

        private void AttachDataContext()
        {
            DetachDataContext();

            _coverArtData = DataContext as CoverArtData;
            if (_coverArtData != null)
            {
                _coverArtData.PropertyChanged += _coverArtData_PropertyChanged;
            }

            UpdateAllDisplayAttributes();
        }

        private void DetachDataContext()
        {
            if (_coverArtData == null)
            {
                return;
            }

            _coverArtData.PropertyChanged -= _coverArtData_PropertyChanged;
            _coverArtData = null;
        }

        private void UpdateAllDisplayAttributes()
        {
        }

        private void _coverArtData_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
        }
    }
}
