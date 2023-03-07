using Model;
using ViewModels;

namespace NewTrade.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductsViewModel.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        public ProductsView()
        {
            InitializeComponent();
        }

        private void OnUpdateProduct(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            IProductsViewModel viewModel = (IProductsViewModel)DataContext;
            AddOrUpdateProductDialog.Update(new((IProduct)e.Parameter, viewModel));
        }

        private void OnAddProduct(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            IProductsViewModel viewModel = (IProductsViewModel)DataContext;
            AddOrUpdateProductDialog.Add(new((IProduct)e.Parameter, viewModel));
        }
    }
}
