using System.Globalization;
using System.Windows.Data;
using System;

namespace NewTrade.Views
{
    public class ImagePathToUriConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string path && (path = path.Trim()).Length > 0 && path[0] == '/')
                return new Uri(@$"pack://application:,,,/NewTrade;component{path}");
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        private ImagePathToUriConverter() { }
        public static ImagePathToUriConverter Insatance { get; } = new();
    }

}
