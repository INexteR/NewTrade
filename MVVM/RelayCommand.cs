using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.Commands
{
    public class RelayCommand : CommandBase
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;
        private readonly string[] _properties;

        public RelayCommand(Action<object?> execute)
        {
            _execute = execute;
            _properties = Array.Empty<string>();
        }

        public RelayCommand(Action<object?> execute, Func<object?, bool> canExecute, INotifyPropertyChanged notify, params string[] properties) : this(execute)
        {
            _canExecute = canExecute;
            _properties = properties;
            notify.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (_properties.Contains(e.PropertyName))
            {
                OnCanExecuteChanged();
            }
        }

        public override void Execute(object? parameter)
        {
            _execute(parameter);
        }

        public override bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }
    }

    public class RelayCommand<T> : RelayCommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool>? _canExecute;

        public RelayCommand(Action<T> execute) : base(null!)
        {
            _execute = execute;
        }
        public RelayCommand(Action<T> execute, Func<T, bool> canExecute, INotifyPropertyChanged notify, params string[] properties) 
            : base(null!, null!, notify, properties)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public override void Execute(object? parameter)
        {
            _execute((T)parameter!);
        }

        public override bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke((T)parameter!) ?? true;
        }
    }
}
