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
        }

        private void OnAddProductClick(object sender, RoutedEventArgs e)
        {
            IProductsViewModel viewModel = (IProductsViewModel)DataContext;
            AddOrUpdateProductDialog.Add(new(null, viewModel));
        }

        private void OnRemoveProductClick(object sender, RoutedEventArgs e)
        {
            IProductsViewModel viewModel = (IProductsViewModel)DataContext;
            FrameworkElement element = (FrameworkElement)sender;
            IProduct product = (IProduct)element.DataContext;

            string message = $"Товар {product.Name} производител {product.Manufacturer.Name} будет удален.";
            if (MessageBox.Show(message, "Удаление товара", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                viewModel.RemoveProduct.TryExecute(product);
            }
        }

        private void OnUpdateProductClick(object sender, RoutedEventArgs e)
        {
            IProductsViewModel viewModel = (IProductsViewModel)DataContext;
            FrameworkElement element = (FrameworkElement)sender;
            IProduct product = (IProduct)element.DataContext;
            AddOrUpdateProductDialog.Update(new(product, viewModel));
        }
    }
}
