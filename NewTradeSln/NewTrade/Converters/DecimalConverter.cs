using System;
using System.Globalization;
using System.Windows.Data;

namespace NewTrade.Converters
{
    public class DecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (decimal)value is -1 ? string.Empty : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = (string)value;
            return s switch
            {
                "" => -1,
                { Length: > 0 } and [.., '.'] => Binding.DoNothing,
                _ => decimal.Parse(s, culture)
            };
        }

        private DecimalConverter() { }

        public static DecimalConverter Instance { get; } = new();
    }
}
