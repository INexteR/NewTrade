﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MVVM.ViewModels
{
    public abstract class ViewModelBase : BaseInpc
    {
        private readonly Dictionary<string, object?> _properties = new();

        protected T? Get<T>([CallerMemberName] string propertyName = "")
        {
            T? value;
            if (_properties.TryGetValue(propertyName, out object? _prop))
            {
                value = (T?)_prop;
            }
            else
            {
                value = default;
            }
            return value;
        }

        protected void Set<T>(T? newValue, [CallerMemberName] string propertyName = "")
        {
            T? oldValue;
            if (_properties.TryGetValue(propertyName, out object? _prop))
            {
                oldValue = (T?)_prop;
            }
            else
            {
                oldValue = default;
            }
            if (!Equals(oldValue, newValue))
            {
                OnPropertyChanging(propertyName, oldValue, newValue);
                OnPropertyChanging(propertyName);
                OnPropertyChanged(propertyName);
                OnPropertyChanged(propertyName, oldValue, newValue);
            }
        }


        protected override void OnPropertyChanging(string propertyName, object? oldValue, object? newValue)
        {
            _properties[propertyName] = newValue;
        }
    }
}
