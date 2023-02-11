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
            locator.LoginViewModel = new(new());
        }
    }
}
