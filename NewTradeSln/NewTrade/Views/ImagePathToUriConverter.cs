using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Markup;

namespace NewTrade.Views
{
    public class ImageNameToPathConverter : IValueConverter
    {
        private const string ImageFolder = "Images";
        private static readonly string ImageFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ImageFolder);
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string name = (string)value;
            if (string.IsNullOrWhiteSpace(name))
                return name;
            return Path.Combine(ImageFolderPath, name);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        private ImageNameToPathConverter() { }
        public static ImageNameToPathConverter Insatance { get; } = new();
    }

    [MarkupExtensionReturnType(typeof(ImageNameToPathConverter))]
    public class ImageNameToPathExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return ImageNameToPathConverter .Insatance;
        }
    }
}
