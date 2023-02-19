
using Interfaces;

namespace NewTrade
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnAppStartup(object sender, StartupEventArgs e)
        {
            Locator locator = (Locator)FindResource(nameof(locator));
            Shop shop = (Shop)FindResource(nameof(shop));
            IAuthorization authorization = shop;
            locator.CurrentViewModel = new LoginViewModel(authorization);
        }
    }
}
