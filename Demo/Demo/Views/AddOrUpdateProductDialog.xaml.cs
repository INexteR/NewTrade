namespace Demo.Views
{
    /// <summary>
    /// Логика взаимодействия для AddOrUpdateProductDialog.xaml
    /// </summary>
    public partial class AddOrUpdateProductDialog : Window
    {
        public AddOrUpdateProductDialog()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnCanExecuteSave(object sender, CanExecuteRoutedEventArgs e)
        {
            // Проеверка возможности сохранения (корректоность product)
            Product product = (Product)e.Parameter;

            e.CanExecute = true; // Какое-то выражение
        }

        private void OnExecutedSave(object sender, ExecutedRoutedEventArgs e)
        {
            // Вызов метода VM 
            BigViewModel.Instance.Save((Product)e.Parameter);
        }

        private void OnChooseImageClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
