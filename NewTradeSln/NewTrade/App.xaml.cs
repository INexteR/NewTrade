using Model;
using ShopSQLite;
using ShopViewModels;

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

        private void OnAppStartup(object sender, StartupEventArgs e)
        {
            locator = (Locator)FindResource(nameof(locator));
            Shop shopModel = new Shop(false);
            this.shopModel = shopModel;
            authorizationModel = shopModel;
            locator.Authorization = new AuthorizationViewModel(authorizationModel);
            locator.Products = new ProductsViewModel(shopModel);

            // Здесь нужно добавить обработку ошибок 
            _ = shopModel
                .LoadDataAsync()
                .ContinueWith(t =>
                {
                    // Маршалинг исключений в поток App
                    if (t.Exception is not null)
                        throw t.Exception;
                });
        }
    }
}
