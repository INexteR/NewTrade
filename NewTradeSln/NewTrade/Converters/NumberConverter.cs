using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace NewTrade.Converters
{
    public class NumberConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueType = value?.GetType();
            return Type.GetTypeCode(valueType) switch
            {
                TypeCode.Decimal => (decimal)value! < 0 ? string.Empty : value,
                TypeCode.Double => (double)value! < 0 ? string.Empty : value,
                TypeCode.SByte => (sbyte)value! < 0 ? string.Empty : value,
                TypeCode.Int16 => (short)value! < 0 ? string.Empty : value,
                TypeCode.Int32 => (int)value! < 0 ? string.Empty : value,
                TypeCode.Int64 => (long)value! < 0 ? string.Empty : value,
                _ => valueType == typeof(Int128) && (Int128)value! < 0
                                  ? string.Empty
                                  : value
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = (string)value;
            if (string.IsNullOrEmpty(s))
                return -1;
            return Type.GetTypeCode(targetType) switch
            {
                TypeCode.Decimal => s[^1] != '.' ? decimal.Parse(s, CultureInfo.InvariantCulture) : Binding.DoNothing,
                TypeCode.Double => s[^1] != '.' ? double.Parse(s, CultureInfo.InvariantCulture) : Binding.DoNothing,
                TypeCode.SByte => sbyte.Parse(s, CultureInfo.InvariantCulture),
                TypeCode.Int16 => short.Parse(s, CultureInfo.InvariantCulture),
                TypeCode.Int32 => int.Parse(s, CultureInfo.InvariantCulture),
                TypeCode.Int64 => long.Parse(s, CultureInfo.InvariantCulture),
                _ => targetType == typeof(Int128)
                                  ? Int128.Parse(s, CultureInfo.InvariantCulture)
                                  : value
            };
        }

        private NumberConverter() { }
        public static NumberConverter Instance { get; } = new NumberConverter();
    }

    [MarkupExtensionReturnType(typeof(NumberConverter))]
    public class NumberConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return NumberConverter.Instance;
        }
    }
}
