using Model;
using ShopSQLite;
using ShopViewModels;
using System.ComponentModel;
using System.Windows.Threading;

namespace NewTrade
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL.
        private IShop shopModel;
        private IAuthorization authorizationModel;
        private Locator locator;
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL.

        private async void OnAppStartup(object sender, StartupEventArgs e)
        {
            locator = (Locator)Resources[nameof(locator)];
            var shopModel = new Shop(/*true*/); // Для пересборки БД - нужно задать true
            this.shopModel = shopModel;
            authorizationModel = shopModel;
            locator.Authorization = new AuthorizationViewModel(authorizationModel);
            locator.Products = new ProductsViewModel(shopModel);

            DispatcherUnhandledException += OnException;
            await shopModel.LoadDataAsync();
        }

        private void OnException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Exception.Message.Msg("Исключение");
            e.Handled = true;
        }

        private void ComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var cBox = (ComboBox)sender;
            var tBtn = (Control)cBox.Template.FindName("toggleButton", cBox);
            var border = (Border)tBtn.Template.FindName("templateRoot", tBtn);
            border.Background = cBox.Background;

            var PART_EditableTextBox = (TextBox)cBox.Template.FindName("PART_EditableTextBox", cBox);
            
            border = (Border)PART_EditableTextBox.Parent;
            border.Background = cBox.Background;
        }

        public static bool IsInDesignMode { get; } = DesignerProperties.GetIsInDesignMode(new DependencyObject());
    }
}