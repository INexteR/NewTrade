namespace Demo.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthorizeView.xaml
    /// </summary>
    public partial class AuthorizeView : UserControl
    {
        private readonly AuthorizationVM authorizationVM;
        public AuthorizeView()
        {
            InitializeComponent();
            authorizationVM = new();
        }

        private void Authorize_Click(object sender, RoutedEventArgs e)
        {
            var user = authorizationVM.Authorize(login.Text, password.Text);
            if (user != null)
            {
                this.Navigate(new ProductsView(user));
            }
            else
            {
                MessageBox.Show("Пользователь не найден");
            }
        }

        private void Guest_Click(object sender, RoutedEventArgs e)
        {
            this.Navigate(new ProductsView());
        }
    }
}
