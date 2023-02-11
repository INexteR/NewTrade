using System.Threading.Tasks;

namespace MVVM.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        public abstract Task ExecuteAsync(object? parameter);

        public override sealed async void Execute(object? parameter)
        {
            IsExecuting = true;
            try
            {
                await ExecuteAsync(parameter);
            }
            finally
            {
                IsExecuting = false;
            }
        }

        public override bool CanExecute(object? parameter)
        {
            return !_isExecuting;
        }

        private bool _isExecuting;
        public bool IsExecuting
        {
            get => _isExecuting;
            private set
            {
                _isExecuting = value;
                OnCanExecuteChanged();
            }
        }
    }
}
