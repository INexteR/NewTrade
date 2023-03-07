using Model;
using NewTrade.Views;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using ViewModels;

namespace Converters
{

    [ValueConversion(typeof(object), typeof(Array))]
    public class ProductArgsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new AddOrUpdateProductDialogArgs((IProduct)values[0], (IProductsViewModel)values[1]); ;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        // Если нет членов экземпляра, то нет и смысла в конструкторе экземпляра.
        private ProductArgsConverter() { }

        public static ProductArgsConverter Instance { get; } = new();
    }

    [MarkupExtensionReturnType(typeof(ProductArgsConverter))]
    public class ProductArgsExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return ProductArgsConverter.Instance;
        }
    }
}
