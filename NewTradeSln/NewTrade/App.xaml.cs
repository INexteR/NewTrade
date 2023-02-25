
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
            shopModel = new Shop();
            authorizationModel = shopModel;
            locator.Authorization = new AuthorizationViewModel(authorizationModel);
            locator.Products = new ProductsViewModel(shopModel);
        }
    }
}
