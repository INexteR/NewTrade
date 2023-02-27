using ShopViewModels;
using System.Windows.Data;

namespace NewTrade.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private static readonly Binding loginBinding = new Binding(nameof(LoginPassword.Login))
        {
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        };
        private static readonly Binding passwordBinding = new Binding(nameof(LoginPassword.Password))
        {
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        };
        private void OnAuthorize(object sender, RoutedEventArgs e)
        {
            loginTBox.SetBinding(TextBox.TextProperty, loginBinding);
            passwordTBox.SetBinding (TextBox.TextProperty, passwordBinding);
        }
    }

}
