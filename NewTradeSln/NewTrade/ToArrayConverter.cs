using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Converters
{

    [ValueConversion(typeof(object), typeof(Array))]
    public class ToArrayConverter : IValueConverter, IMultiValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new object[] { value };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            object[] copy = new object[values.Length];
            values.CopyTo(copy, 0);
            return copy;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        // Если нет членов экземпляра, то нет и смысла в конструкторе экземпляра.
        private ToArrayConverter() { }

        public static ToArrayConverter Instance { get; } = new();
    }

    [MarkupExtensionReturnType(typeof(ToArrayConverter))]
    public class ToArrayConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return ToArrayConverter.Instance;
        }
    }
}
