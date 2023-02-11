using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MVVM.Commands
{
    public class AsyncRelayCommand : AsyncCommandBase
    {
        private readonly Func<object?, Task> _execute;
        private readonly Func<object?, bool>? _canExecute;
        private readonly string[] _properties;

        public AsyncRelayCommand(Func<object?, Task> execute)
        {
            _execute = execute;
            _properties = Array.Empty<string>();
        }

        public AsyncRelayCommand(Func<object?, Task> execute, Func<object?, bool> canExecute, INotifyPropertyChanged notify, params string[] properties) : this(execute)
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

        public override async Task ExecuteAsync(object? parameter)
        {
            await _execute(parameter);
        }

        public override bool CanExecute(object? parameter)
        {
            return (_canExecute?.Invoke(parameter) ?? true) && !IsExecuting;
        }
    }
    public class AsyncRelayCommand<T> : AsyncRelayCommand
    {
        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool>? _canExecute;

        public AsyncRelayCommand(Func<T, Task> execute) : base(null!)
        {
            _execute = execute;
        }

        public AsyncRelayCommand(Func<T, Task> execute, Func<T, bool> canExecute, INotifyPropertyChanged notify, params string[] properties) 
            : base(null!, null!, notify, properties)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            await _execute((T)parameter!);
        }

        public override bool CanExecute(object? parameter)
        {
            return (_canExecute?.Invoke((T)parameter!) ?? true) && !IsExecuting;
        }
    }
}
