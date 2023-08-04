using System;
using System.Globalization;
using System.Windows.Data;

namespace NewTrade.Converters
{
    public class DigitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return int.Parse(value.ToString()!) is -1 ? string.Empty : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value is "" ? -1 : value;
        }

        private DigitConverter() { }

        public static DigitConverter Instance { get; } = new();
    }
}
