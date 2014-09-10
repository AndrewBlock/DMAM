using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

using DMAM.Album.Data.Models;
using DMAM.Controls;

namespace DMAM.Album.Editors
{
    public class TrackEditor : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource",
            typeof(IEnumerable<TrackData>), typeof(TrackEditor), new PropertyMetadata(null, NotifyPropertyChanged));

        public static readonly DependencyProperty HorizontalGapMarginProperty = DependencyProperty.Register("HorizontalGapMargin",
            typeof(double), typeof(TrackEditor), new PropertyMetadata(5d, NotifyPropertyChanged));

        public static readonly DependencyProperty VerticalGapMarginProperty = DependencyProperty.Register("VerticalGapMargin",
            typeof(double), typeof(TrackEditor), new PropertyMetadata(5d, NotifyPropertyChanged));

        public static readonly DependencyProperty FieldSetVerticalGapMarginProperty = DependencyProperty.Register("FieldSetVerticalGapMargin",
            typeof(double), typeof(TrackEditor), new PropertyMetadata(0d, NotifyPropertyChanged));

        public static readonly DependencyProperty LabelFontSizeFactorProperty = DependencyProperty.Register("LabelFontSizeFactor",
            typeof(double), typeof(TrackEditor), new PropertyMetadata(1.5d, NotifyPropertyChanged));

        public static readonly DependencyProperty SectionPaddingProperty = DependencyProperty.Register("SectionPadding",
            typeof(Thickness), typeof(TrackEditor), new PropertyMetadata(new Thickness(5d, 5d, 5d, 5d), NotifyPropertyChanged));

        public static readonly DependencyProperty SectionBackgroundProperty = DependencyProperty.Register("SectionBackground",
            typeof(Brush), typeof(TrackEditor), new PropertyMetadata(new SolidColorBrush(Colors.Transparent), NotifyPropertyChanged));

        public static readonly DependencyProperty FieldNormalBackgroundProperty = DependencyProperty.Register("FieldNormalBackground",
            typeof(Brush), typeof(TrackEditor), new PropertyMetadata(new SolidColorBrush(Colors.Transparent), NotifyPropertyChanged));

        public static readonly DependencyProperty FieldModifiedBackgroundProperty = DependencyProperty.Register("FieldModifiedBackground",
            typeof(Brush), typeof(TrackEditor), new PropertyMetadata(new SolidColorBrush(Colors.LightYellow), NotifyPropertyChanged));

        private const string TrackNumberPropertyName = "TrackNumber";
        private const string MetadataFieldsPropertyName = "MetadataFields";
        private const string TrackLengthPropertyName = "TrackLength";

        private Dictionary<TrackData, TextBlock> _numberEditors = new Dictionary<TrackData, TextBlock>();
        private Dictionary<TrackData, FieldSetEditor> _fieldSetEditors = new Dictionary<TrackData, FieldSetEditor>();
        private Dictionary<TrackData, TextBlock> _lengthEditors = new Dictionary<TrackData, TextBlock>();

        private Grid _layoutRoot = new Grid();

        private IEnumerable<TrackData> _itemsSource;
        private INotifyCollectionChanged _notifyCollectionChanged;

        public TrackEditor()
        {
            TextOptions.SetTextFormattingMode(this, TextFormattingMode.Display);
            TextOptions.SetTextRenderingMode(this, TextRenderingMode.ClearType);

            Content = _layoutRoot;
            AttachFieldSet(ItemsSource);
        }

        public IEnumerable<TrackData> ItemsSource
        {
            get
            {
                return (IEnumerable<TrackData>) GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public double HorizontalGapMargin
        {
            get
            {
                return (double) GetValue(HorizontalGapMarginProperty);
            }
            set
            {
                SetValue(HorizontalGapMarginProperty, value);
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

        public double FieldSetVerticalGapMargin
        {
            get
            {
                return (double) GetValue(FieldSetVerticalGapMarginProperty);
            }
            set
            {
                SetValue(FieldSetVerticalGapMarginProperty, value);
            }
        }

        public double LabelFontSizeFactor
        {
            get
            {
                return (double) GetValue(LabelFontSizeFactorProperty);
            }
            set
            {
                SetValue(LabelFontSizeFactorProperty, value);
            }
        }

        public Thickness SectionPadding
        {
            get
            {
                return (Thickness) GetValue(SectionPaddingProperty);
            }
            set
            {
                SetValue(SectionPaddingProperty, value);
            }
        }

        public Brush SectionBackground
        {
            get
            {
                return (Brush) GetValue(SectionBackgroundProperty);
            }
            set
            {
                SetValue(SectionBackgroundProperty, value);
            }
        }

        public Brush FieldNormalBackground
        {
            get
            {
                return (Brush) GetValue(FieldNormalBackgroundProperty);
            }
            set
            {
                SetValue(FieldNormalBackgroundProperty, value);
            }
        }

        public Brush FieldModifiedBackground
        {
            get
            {
                return (Brush) GetValue(FieldModifiedBackgroundProperty);
            }
            set
            {
                SetValue(FieldModifiedBackgroundProperty, value);
            }
        }

        private static void NotifyPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var editor = (TrackEditor) dependencyObject;
            editor.NotifyPropertyChanged(args);
        }

        private void NotifyPropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            if (args.Property == ItemsSourceProperty)
            {
                DetachFieldSet();
                AttachFieldSet(ItemsSource);
            }
            else if ((args.Property == HorizontalGapMarginProperty)
                || (args.Property == VerticalGapMarginProperty))
            {
                ClearControlLayout();
                UpdateControlLayout();
            }
            else if ((args.Property == FieldSetVerticalGapMarginProperty)
                || (args.Property == LabelFontSizeFactorProperty)
                || (args.Property == SectionPaddingProperty)
                || (args.Property == SectionBackgroundProperty)
                || (args.Property == FieldNormalBackgroundProperty)
                || (args.Property == FieldModifiedBackgroundProperty))
            {
                ClearControlLayout();
                ReleaseEditors();
                LoadEditors();
                UpdateControlLayout();
            }
        }

        private void AttachFieldSet(IEnumerable<TrackData> itemsSource)
        {
            _itemsSource = itemsSource;
            if (_itemsSource == null)
            {
                return;
            }

            _notifyCollectionChanged = _itemsSource as INotifyCollectionChanged;
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

            _itemsSource = null;
        }

        private void LoadEditors()
        {
            if (_itemsSource == null)
            {
                return;
            }

            foreach (var trackData in _itemsSource)
            {
                LoadNumberEditor(trackData);
                LoadFieldSetEditor(trackData);
                LoadLengthEditor(trackData);
            }
        }

        private void ReleaseEditors()
        {
            ReleaseNumberEditors();
            ReleaseFieldSetEditors();
            ReleaseLengthEditors();
        }

        private void UpdateControlLayout()
        {
            if (_itemsSource == null)
            {
                return;
            }

            SetColumnDefinitions();

            var rowIndex = 0;
            foreach (var trackData in _itemsSource)
            {
                SetControlRowLayout(trackData, ref rowIndex);
            }
        }

        private const int NumberEditorColumnIndex = 0;
        private const int FieldSetEditorColumnIndex = 2;
        private const int LengthEditorColumnIndex = 4;

        private void SetColumnDefinitions()
        {
            AddColumn(0d, GridUnitType.Auto);
            AddColumn(HorizontalGapMargin, GridUnitType.Pixel);
            AddColumn(1d, GridUnitType.Star);
            AddColumn(HorizontalGapMargin, GridUnitType.Pixel);
            AddColumn(0d, GridUnitType.Auto);
        }

        private void AddColumn(double value, GridUnitType gridUnitType)
        {
            _layoutRoot.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(value, gridUnitType)
            });
        }

        private void AddRow(double value, GridUnitType gridUnitType)
        {
            _layoutRoot.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(value, gridUnitType)
            });
        }

        private void SetControlRowLayout(TrackData trackData, ref int rowIndex)
        {
            if (rowIndex != 0)
            {
                AddRow(VerticalGapMargin, GridUnitType.Pixel);
                rowIndex++;
            }

            AddRow(0d, GridUnitType.Auto);

            var numberEditor = _numberEditors[trackData];
            _layoutRoot.Children.Add(numberEditor);
            Grid.SetColumn(numberEditor, NumberEditorColumnIndex);
            Grid.SetRow(numberEditor, rowIndex);

            var fieldSetEditor = _fieldSetEditors[trackData];
            _layoutRoot.Children.Add(fieldSetEditor);
            Grid.SetColumn(fieldSetEditor, FieldSetEditorColumnIndex);
            Grid.SetRow(fieldSetEditor, rowIndex);

            var lengthEditor = _lengthEditors[trackData];
            _layoutRoot.Children.Add(lengthEditor);
            Grid.SetColumn(lengthEditor, LengthEditorColumnIndex);
            Grid.SetRow(lengthEditor, rowIndex);

            rowIndex++;
        }

        private void ClearControlLayout()
        {
            _layoutRoot.Children.Clear();
            _layoutRoot.RowDefinitions.Clear();
            _layoutRoot.ColumnDefinitions.Clear();
        }

        private void _notifyCollectionChanged_CollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            ClearControlLayout();
            ReleaseEditors();
            LoadEditors();
            UpdateControlLayout();
        }

        private void LoadNumberEditor(TrackData trackData)
        {
            var numberEditor = new TextBlock
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = SectionPadding,
                FontSize = FontSize * LabelFontSizeFactor,
                FontWeight = FontWeights.Bold,
                Background = SectionBackground,
                DataContext = trackData
            };

            numberEditor.SetBinding(TextBlock.TextProperty, new Binding
            {
                Path = new PropertyPath(TrackNumberPropertyName)
            });

            _numberEditors.Add(trackData, numberEditor);
        }

        private void LoadFieldSetEditor(TrackData trackData)
        {
            var fieldSetEditor = new FieldSetEditor
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                VerticalGapMargin = FieldSetVerticalGapMargin,
                Padding = SectionPadding,
                Background = SectionBackground,
                Foreground = Foreground,
                NormalBackground = FieldNormalBackground,
                ModifiedBackground = FieldModifiedBackground,
                DataContext = trackData
            };

            fieldSetEditor.SetBinding(FieldSetEditor.FieldSetProperty, new Binding
            {
                Path = new PropertyPath(MetadataFieldsPropertyName)
            });

            _fieldSetEditors.Add(trackData, fieldSetEditor);
        }

        private void LoadLengthEditor(TrackData trackData)
        {
            var lengthEditor = new TextBlock
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = SectionPadding,
                FontSize = FontSize * LabelFontSizeFactor,
                FontWeight = FontWeights.Bold,
                Background = SectionBackground,
                DataContext = trackData
            };

            lengthEditor.SetBinding(TextBlock.TextProperty, new Binding
            {
                Path = new PropertyPath(TrackLengthPropertyName)
            });

            _lengthEditors.Add(trackData, lengthEditor);
        }

        private void ReleaseNumberEditors()
        {
            foreach (var numberEditor in _numberEditors.Values)
            {
                numberEditor.DataContext = null;
            }

            _numberEditors.Clear();
        }

        private void ReleaseFieldSetEditors()
        {
            foreach (var fieldSetEditor in _fieldSetEditors.Values)
            {
                fieldSetEditor.DataContext = null;
            }

            _fieldSetEditors.Clear();
        }

        private void ReleaseLengthEditors()
        {
            foreach (var lengthEditor in _lengthEditors.Values)
            {
                lengthEditor.DataContext = null;
            }

            _lengthEditors.Clear();
        }
    }
}
