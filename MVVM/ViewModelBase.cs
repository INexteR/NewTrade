using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        protected ViewModelBase() { }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void Set<T>(ref T storage, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(storage, newValue))
            { 
                T oldValue = storage;
                storage = newValue;
                OnPropertyChanges(propertyName, oldValue!, newValue!);
                OnPropertyChanged(propertyName); 
                OnPropertyChanged(propertyName, oldValue!, newValue!);                              
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new(propertyName));
        }

        protected static readonly PropertyChangedEventArgs allProperties = new(string.Empty);

        protected void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        protected virtual void OnPropertyChanges(string propertyName, object oldValue, object newValue) { }
        protected virtual void OnPropertyChanged(string propertyName, object oldValue, object newValue) { }
    }
}
