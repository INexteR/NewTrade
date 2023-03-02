using Model;
using ShopSQLite;
using ShopViewModels;
using System;
using System.Threading.Tasks;
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
            locator = (Locator)FindResource(nameof(locator));
            var shopModel = new Shop(true); // Для пересборки БД - нужно задать true
            this.shopModel = shopModel;
            authorizationModel = shopModel;
            locator.Authorization = new AuthorizationViewModel(authorizationModel);
            locator.Products = new ProductsViewModel(shopModel);

            // Здесь нужно добавить обработку ошибок 
            DispatcherUnhandledException += OnException;
            await shopModel.LoadDataAsync();
        }

        private void OnException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.ToString(), "Исключение");
        }
    }
}
