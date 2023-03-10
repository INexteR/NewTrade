namespace Demo.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthorizeView.xaml
    /// </summary>
    public partial class AuthorizeView : UserControl
    {
        private BigViewModel viewModel = null!;
        public AuthorizeView()
        {
            InitializeComponent();
        }

        private void Authorize_Click(object sender, RoutedEventArgs e)
        {
            var user = viewModel.Authorize(login.Text, password.Text);
            if (user != null)
            {
                this.Navigate<ProductsView>();
            }
            else
            {
                MessageBox.Show("Пользователь не найден", null);
            }
        }

        private void Guest_Click(object sender, RoutedEventArgs e)
        {
            this.Navigate<ProductsView>();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel = (BigViewModel)DataContext;
        }
    }
}
