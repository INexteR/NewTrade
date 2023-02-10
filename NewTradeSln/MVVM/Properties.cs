using MVVM.ViewModels;
using System.Collections.Generic;

namespace MVVM.ViewModels
{
    public abstract class Properties : ViewModelBase
    {
        private readonly Dictionary<string, object> _properties;

        protected Properties()
        {
            _properties = new();
        }

        public abstract bool HasErrors { get; }

        public T Get<T>(string propertyName) => (T)_properties[propertyName];

        protected override void OnPropertyChanges(string propertyName, object oldValue, object newValue)
        {
            _properties[propertyName] = newValue;
        }
    }
}
