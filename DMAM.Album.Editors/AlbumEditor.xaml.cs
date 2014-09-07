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
    public partial class AlbumEditor : IDisposable
    {
        private AlbumData _albumData;

        public AlbumEditor()
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

            _albumData = DataContext as AlbumData;
            if (_albumData != null)
            {
                _albumData.PropertyChanged += _albumData_PropertyChanged;
            }

            UpdateAllDisplayAttributes();
        }

        private void DetachDataContext()
        {
            if (_albumData == null)
            {
                return;
            }

            _albumData.PropertyChanged -= _albumData_PropertyChanged;
            _albumData = null;
        }

        private void UpdateAllDisplayAttributes()
        {
        }

        private void _albumData_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
        }
    }
}
