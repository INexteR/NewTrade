
namespace NewTrade.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class AuthorizationView : UserControl
    {
        public AuthorizationView()
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
