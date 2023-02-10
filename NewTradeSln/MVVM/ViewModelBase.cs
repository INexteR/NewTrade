using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM.ViewModels
{
    public abstract class BaseInpc : INotifyPropertyChanged, INotifyPropertyChanging
    {
        protected BaseInpc() { }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event PropertyChangingEventHandler? PropertyChanging;

        protected void Set<T>(ref T? storage, T? newValue, [CallerMemberName] string propertyName = "")
        {
            if (!Equals(storage, newValue)) // В WPF для сравненияиспользуется метод object.Equals(object).
            {
                T? oldValue = storage;
                storage = newValue;
                OnPropertyChanging(propertyName, oldValue, newValue);
                OnPropertyChanging(propertyName);
                OnPropertyChanged(propertyName);
                OnPropertyChanged(propertyName, oldValue, newValue);
            }
        }

        public static PropertyChangedEventArgs AllChanged { get; } = new(string.Empty);
        public static PropertyChangingEventArgs AllChanging { get; } = new(string.Empty);

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventArgs args = AllChanged;
            if (!string.IsNullOrWhiteSpace(propertyName))
                args = new(propertyName);
            PropertyChanged?.Invoke(this, args);
        }
        protected void OnPropertyChanging([CallerMemberName] string propertyName = "")
        {
            PropertyChangingEventArgs args = AllChanging;
            if (!string.IsNullOrWhiteSpace(propertyName))
                args = new(propertyName);
            PropertyChanging?.Invoke(this, args);
        }

        protected static readonly PropertyChangedEventArgs allProperties = new(string.Empty);

        protected void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        protected virtual void OnPropertyChanging(string propertyName, object? oldValue, object? newValue) { }
        protected virtual void OnPropertyChanged(string propertyName, object? oldValue, object? newValue) { }
    }
}
