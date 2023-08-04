using System;
using System.Globalization;
using System.Windows.Data;

namespace NewTrade.Converters
{
    public class IdConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value is -1? null : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is null ? -1 : value;
        }

        private IdConverter() { }

        public static IdConverter Instance { get; } = new();
    }
}
