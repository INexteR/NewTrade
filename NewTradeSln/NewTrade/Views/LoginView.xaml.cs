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

        private void OnEnabledValidation(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            Validation.SetErrorTemplate(tb, Validation.GetErrorTemplate(this));
            tb.TextChanged -= OnEnabledValidation;
        }
    }
}
