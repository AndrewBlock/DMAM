using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using DMAM.Core.DataModels;

namespace DMAM.Controls
{
    public partial class FieldValueEditor : IDisposable
    {
        private const string ValuePropertyName = "Value";
        private const string IsReadOnlyPropertyName = "IsReadOnly";
        private const string IsModifiedPropertyName = "IsModified";

        private const double VisibleOpacity = 1.0;
        private const double HiddenOpacity = 0.0;
        private const double EnabledOpacity = 1.0;
        private const double DisabledOpacity = 0.4;

        private static readonly FontWeight PrimaryFontWeight = FontWeights.DemiBold;
        private const double PrimaryOpacity = 1.0;
        private static readonly FontWeight SecondaryFontWeight = FontWeights.Normal;
        private const double SecondaryOpacity = 1.0;
        private static readonly FontWeight GeneralFontWeight = FontWeights.Normal;
        private const double GeneralOpacity = 0.70;

        private FieldValue _fieldValue;

        private bool _mouseEntered;
        private bool _isModifiedHighlighted;
        private static readonly Brush _normalBackgroundBrush = new SolidColorBrush(Colors.Transparent);
        private static readonly Brush _modifiedBackgroundBrush = new SolidColorBrush(Colors.AliceBlue);

        private bool _valueUpdateInProgress;

        public FieldValueEditor()
        {
            InitializeComponent();

            editText.TextChanged += textBox_TextChanged;
            editText.GotFocus += textBox_GotFocus;
            editText.LostFocus += textBox_LostFocus;

            AttachDataContext();
            DataContextChanged += FieldValueEditor_DataContextChanged;

            UpdateActivationState();
        }

        public void Dispose()
        {
            DataContextChanged -= FieldValueEditor_DataContextChanged;
            DetachDataContext();

            editText.LostFocus -= textBox_LostFocus;
            editText.GotFocus -= textBox_GotFocus;
            editText.TextChanged -= textBox_TextChanged;
        }

        protected override void OnMouseEnter(MouseEventArgs args)
        {
            base.OnMouseEnter(args);
            _mouseEntered = true;
            UpdateActivationState();
        }

        protected override void OnMouseLeave(MouseEventArgs args)
        {
            base.OnMouseLeave(args);
            _mouseEntered = false;
            UpdateActivationState();
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs args)
        {
            UpdateActivationState();
            UpdateEmptyTextVisibility();
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateActivationState();
            UpdateEmptyTextVisibility();
            editText.SelectAll();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            readonlyText.Text = editText.Text;
            UpdateEmptyTextVisibility();

            if ((_fieldValue == null) || (_valueUpdateInProgress))
            {
                return;
            }

            try
            {
                _valueUpdateInProgress = true;
                _fieldValue.Value = editText.Text;
            }
            finally
            {
                _valueUpdateInProgress = false;
            }
        }

        private void FieldValueEditor_DataContextChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            AttachDataContext();
        }

        private void _fieldValue_PropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == ValuePropertyName)
            {
                UpdateValue();
            }
            else if (args.PropertyName == IsReadOnlyPropertyName)
            {
                UpdateIsReadOnly();
            }
            else if (args.PropertyName == IsModifiedPropertyName)
            {
                UpdateIsModified();
            }
        }

        private void RevertCommand_CanExecuteChanged(object sender, EventArgs args)
        {
            UpdateRevertCommand();
        }

        private void RemoveCommand_CanExecuteChanged(object sender, EventArgs args)
        {
            UpdateRemoveCommand();
        }

        private void AttachDataContext()
        {
            DetachDataContext();

            _fieldValue = DataContext as FieldValue;
            if (_fieldValue != null)
            {
                _fieldValue.PropertyChanged += _fieldValue_PropertyChanged;
                _fieldValue.RemoveCommand.CanExecuteChanged += RemoveCommand_CanExecuteChanged;
                _fieldValue.RevertCommand.CanExecuteChanged += RevertCommand_CanExecuteChanged;
            }

            UpdateAllDisplayAttributes();
        }

        private void DetachDataContext()
        {
            if (_fieldValue == null)
            {
                return;
            }

            _fieldValue.RevertCommand.CanExecuteChanged -= RevertCommand_CanExecuteChanged;
            _fieldValue.RemoveCommand.CanExecuteChanged -= RemoveCommand_CanExecuteChanged;
            _fieldValue.PropertyChanged -= _fieldValue_PropertyChanged;
            _fieldValue = null;
        }

        private void UpdateAllDisplayAttributes()
        {
            UpdateTextOpacity();
            UpdateValue();
            UpdateToolTip();
            UpdateIsReadOnly();
            UpdateIsModified();
            UpdateRevertCommand();
            UpdateRemoveCommand();
        }

        private void UpdateTextOpacity()
        {
            if (_fieldValue == null)
            {
                return;
            }

            FontWeight fontWeight;
            double opacity;

            switch (_fieldValue.Rank)
            {
                case FieldValueRank.Primary:
                {
                    fontWeight = PrimaryFontWeight;
                    opacity = PrimaryOpacity;
                    break;
                }
                case FieldValueRank.Secondary:
                {
                    fontWeight = SecondaryFontWeight;
                    opacity = SecondaryOpacity;
                    break;
                }
                default:
                {
                    fontWeight = GeneralFontWeight;
                    opacity = GeneralOpacity;
                    break;
                }
            }

            editText.FontWeight = fontWeight;
            editText.Opacity = opacity;
            readonlyText.FontWeight = fontWeight;
            readonlyText.Opacity = opacity;
        }

        private void UpdateValue()
        {
            if ((_fieldValue == null) || (_valueUpdateInProgress))
            {
                return;
            }

            try
            {
                _valueUpdateInProgress = true;
                editText.Text = _fieldValue.Value;
                editText.SelectAll();
            }
            finally
            {
                _valueUpdateInProgress = false;
            }
        }

        private void UpdateToolTip()
        {
            var value = (_fieldValue != null) ? _fieldValue.DisplayName : string.Empty;
            emptyText.Text = value;
            ToolTipService.SetToolTip(this, value);
        }

        private void UpdateIsReadOnly()
        {
            if ((_fieldValue == null) || (_fieldValue.IsReadOnly))
            {
                editText.Visibility = Visibility.Collapsed;
                readonlyText.Visibility = Visibility.Visible;
            }
            else
            {
                readonlyText.Visibility = Visibility.Collapsed;
                editText.Visibility = Visibility.Visible;
            }
        }

        private void UpdateIsModified()
        {
            _isModifiedHighlighted = (_fieldValue != null) ? _fieldValue.IsModified : false;
            Background = _isModifiedHighlighted ? _modifiedBackgroundBrush : _normalBackgroundBrush;
        }

        private void UpdateRevertCommand()
        {
            var isEnabled = (_fieldValue != null) && (_fieldValue.RevertCommand.CanExecute(null));
            revertButton.IsEnabled = isEnabled;
            revertImage.Opacity = isEnabled ? EnabledOpacity : DisabledOpacity;
        }

        private void UpdateRemoveCommand()
        {
            var isEnabled = (_fieldValue != null) && (_fieldValue.RemoveCommand.CanExecute(null));
            removeButton.IsEnabled = isEnabled;
            removeImage.Opacity = isEnabled ? EnabledOpacity : DisabledOpacity;
        }

        private void UpdateEmptyTextVisibility()
        {
            emptyText.Visibility = ((!editText.IsFocused) && (string.IsNullOrEmpty(editText.Text))) ?
                Visibility.Visible : Visibility.Hidden;
        }

        private void UpdateActivationState()
        {
            if ((_mouseEntered) || (editText.IsFocused))
            {
                SetButtonOpacity(VisibleOpacity);
            }
            else
            {
                SetButtonOpacity(HiddenOpacity);
            }
        }

        private void SetButtonOpacity(double opacity)
        {
            revertButton.Opacity = opacity;
            removeButton.Opacity = opacity;
        }
    }
}
