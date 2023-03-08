
using System;
using System.Globalization;
using System.Windows.Data;

namespace NewTrade.Converters
{
    public class IdToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value - 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value + 1;
        }

        private IdToIndexConverter() { }

        public static IdToIndexConverter Instance { get; } = new();
    }
}
