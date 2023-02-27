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

        private void OnEnabledValidation(object sender, TextChangedEventArgs e)
        {
            if (sender == loginTBox)
            {
                loginTBox.SetBinding(TextBox.TextProperty, loginBinding);
                loginTBox.TextChanged -= OnEnabledValidation;
            }
            else if (sender == passwordTBox)
            {
                passwordTBox.SetBinding(TextBox.TextProperty, passwordBinding);
                passwordTBox.TextChanged -= OnEnabledValidation;
            }
        }
    }
}
