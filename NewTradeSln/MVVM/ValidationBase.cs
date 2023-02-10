using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.ViewModels
{
    public abstract class ValidationBase : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors;

        protected ValidationBase()
        {
            _errors = new();
        }

        public void AddError(string errorMessage, [CallerMemberName]string propertyName = "")
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors.Add(propertyName, new());
            }
            _errors[propertyName].Add(errorMessage);
            OnErrorsChanged(propertyName);
        }

        public void ClearErrors([CallerMemberName] string propertyName = "")
        {
            if (_errors.Remove(propertyName))
            {
                OnErrorsChanged(propertyName);
            }
            OnErrorsChanged(propertyName); 
        }

        public bool HasErrors => _errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            return _errors!.GetValueOrDefault(propertyName)!;
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new(propertyName));
        }
    }
}
