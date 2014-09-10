using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using DMAM.Core.DataModels;

namespace DMAM.Controls
{
    public class FieldSetEditor : UserControl
    {
        public static readonly DependencyProperty FieldSetProperty = DependencyProperty.Register("FieldSet",
            typeof(FieldSet), typeof(FieldSetEditor), new PropertyMetadata(null, NotifyPropertyChanged));

        public static readonly DependencyProperty VerticalGapMarginProperty = DependencyProperty.Register("VerticalGapMargin",
            typeof(double), typeof(FieldSetEditor), new PropertyMetadata(2d, NotifyPropertyChanged));

        public static readonly DependencyProperty NormalBackgroundProperty = DependencyProperty.Register("NormalBackground",
            typeof(Brush), typeof(FieldSetEditor), new PropertyMetadata(new SolidColorBrush(Colors.Transparent), NotifyPropertyChanged));

        public static readonly DependencyProperty ModifiedBackgroundProperty = DependencyProperty.Register("ModifiedBackground",
            typeof(Brush), typeof(FieldSetEditor), new PropertyMetadata(new SolidColorBrush(Colors.LightYellow), NotifyPropertyChanged));

        private Dictionary<FieldValue, FieldValueEditor> _editorLookup =
            new Dictionary<FieldValue, FieldValueEditor>();

        private Grid _layoutRoot = new Grid();

        private FieldSet _fieldSet;
        private INotifyCollectionChanged _notifyCollectionChanged;

        public FieldSetEditor()
        {
            Content = _layoutRoot;
            AttachFieldSet(FieldSet);
        }

        public FieldSet FieldSet
        {
            get
            {
                return (FieldSet) GetValue(FieldSetProperty);
            }
            set
            {
                SetValue(FieldSetProperty, value);
            }
        }

        public double VerticalGapMargin
        {
            get
            {
                return (double) GetValue(VerticalGapMarginProperty);
            }
            set
            {
                SetValue(VerticalGapMarginProperty, value);
            }
        }

        public Brush NormalBackground
        {
            get
            {
                return (Brush) GetValue(NormalBackgroundProperty);
            }
            set
            {
                SetValue(NormalBackgroundProperty, value);
            }
        }

        public Brush ModifiedBackground
        {
            get
            {
                return (Brush) GetValue(ModifiedBackgroundProperty);
            }
            set
            {
                SetValue(ModifiedBackgroundProperty, value);
            }
        }

        private static void NotifyPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var editor = (FieldSetEditor) dependencyObject;
            editor.NotifyPropertyChanged(args);
        }

        private void NotifyPropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            if ((args.Property == FieldSetProperty)
                || (args.Property == NormalBackgroundProperty)
                || (args.Property == ModifiedBackgroundProperty))
            {
                DetachFieldSet();
                AttachFieldSet(FieldSet);
            }
            else if (args.Property == VerticalGapMarginProperty)
            {
                ClearControlLayout();
                UpdateControlLayout();
            }
        }

        private void AttachFieldSet(FieldSet fieldSet)
        {
            _fieldSet = fieldSet;
            if (_fieldSet == null)
            {
                return;
            }

            _notifyCollectionChanged = _fieldSet.FieldValues as INotifyCollectionChanged;
            if (_notifyCollectionChanged != null)
            {
                _notifyCollectionChanged.CollectionChanged
                    += _notifyCollectionChanged_CollectionChanged;
            }

            LoadEditors();
            UpdateControlLayout();
        }

        private void DetachFieldSet()
        {
            ClearControlLayout();
            ReleaseEditors();

            if (_notifyCollectionChanged != null)
            {
                _notifyCollectionChanged.CollectionChanged
                -= _notifyCollectionChanged_CollectionChanged;
                _notifyCollectionChanged = null;
            }

            _fieldSet = null;
        }

        private void LoadEditors()
        {
            foreach (var fieldValue in _fieldSet.FieldValues)
            {
                var fieldValueEditor = new FieldValueEditor
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = Foreground,
                    NormalBackground = NormalBackground,
                    ModifiedBackground = ModifiedBackground
                };

                fieldValueEditor.DataContext = fieldValue;
                _editorLookup.Add(fieldValue, fieldValueEditor);
            }
        }

        private void ReleaseEditors()
        {
            foreach (var fieldValueEditor in _editorLookup.Values)
            {
                fieldValueEditor.DataContext = null;
            }

            _editorLookup.Clear();
        }

        private void UpdateControlLayout()
        {
            if (_fieldSet == null)
            {
                return;
            }

            var rowIndex = 0;

            foreach (var fieldValue in _fieldSet.FieldValues)
            {
                if (rowIndex != 0)
                {
                    _layoutRoot.RowDefinitions.Add(new RowDefinition
                    {
                        Height = new GridLength(VerticalGapMargin, GridUnitType.Pixel)
                    });

                    rowIndex++;
                }

                _layoutRoot.RowDefinitions.Add(new RowDefinition
                {
                    Height = new GridLength(0d, GridUnitType.Auto)
                });

                var fieldValueEditor = _editorLookup[fieldValue];
                _layoutRoot.Children.Add(fieldValueEditor);

                Grid.SetRow(fieldValueEditor, rowIndex);
                rowIndex++;
            }
        }

        private void ClearControlLayout()
        {
            _layoutRoot.Children.Clear();
            _layoutRoot.RowDefinitions.Clear();
        }

        private void _notifyCollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            ClearControlLayout();
            ReleaseEditors();
            LoadEditors();
            UpdateControlLayout();
        }
    }
}
