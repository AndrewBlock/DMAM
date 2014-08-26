using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;

namespace DMAM.Core.MVVM
{
    public class Command : ICommand, IDisposable
    {
        private readonly object _target;

        private string _propertyName;
        private PropertyInfo _propertyInfo;
        private INotifyPropertyChanged _targetNotifyPropertyChanged;

        private MethodInfo _methodInfo;
        private bool _methodUsesParameter;

        public Command(object target, string propertyName, string methodName)
        {
            _target = target;
            InitializeProperty(propertyName);
            InitializeMethod(methodName);
        }

        public void Dispose()
        {
            if (_targetNotifyPropertyChanged != null)
            {
                _targetNotifyPropertyChanged.PropertyChanged -= _target_PropertyChanged;
                _targetNotifyPropertyChanged = null;
            }
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_propertyInfo == null)
            {
                return true;
            }

            return (bool) _propertyInfo.GetValue(_target);
        }

        public void Execute(object parameter)
        {
            if (_methodInfo == null)
            {
                return;
            }

            try
            {
                if (_methodUsesParameter)
                {
                    _methodInfo.Invoke(_target, new object[] { parameter });
                }
                else
                {
                    _methodInfo.Invoke(_target, new object[] { });
                }
            }
            catch (TargetInvocationException exception)
            {
                throw exception.InnerException;
            }
        }

        private void InitializeProperty(string propertyName)
        {
            if ((_target == null) || (string.IsNullOrWhiteSpace(propertyName)))
            {
                return;
            }

            _propertyName = propertyName;

            var propertyInfo = _target.GetType().GetProperty(_propertyName);
            if ((propertyInfo == null) || (propertyInfo.PropertyType != typeof(bool)))
            {
                return;
            }

            _propertyInfo = propertyInfo;

            _targetNotifyPropertyChanged = _target as INotifyPropertyChanged;
            if (_targetNotifyPropertyChanged != null)
            {
                _targetNotifyPropertyChanged.PropertyChanged += _target_PropertyChanged;
            }
        }

        private void _target_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if ((CanExecuteChanged != null) && (e.PropertyName == _propertyName))
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }

        private void InitializeMethod(string methodName)
        {
            if ((_target == null) || (string.IsNullOrWhiteSpace(methodName)))
            {
                return;
            }

            var targetType = _target.GetType();

            var methodInfo = targetType.GetMethod(methodName, new Type[] { typeof(object) });
            if (methodInfo != null)
            {
                _methodInfo = methodInfo;
                _methodUsesParameter = true;
                return;
            }

            methodInfo = targetType.GetMethod(methodName, new Type[] { });
            if (methodInfo != null)
            {
                _methodInfo = methodInfo;
                _methodUsesParameter = false;
            }
        }
    }
}
