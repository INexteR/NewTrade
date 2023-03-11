using Model;
using System;
using ViewModels;

namespace NewTrade.Views
{
    public static class ProductsViewHelper
    {
        public static RoutedEventHandler OnAddProductClick { get; } = (sender, e) =>
        {
            FrameworkElement element = (FrameworkElement)sender;
            IProductsViewModel viewModel = element.FindData<IProductsViewModel>() ?? throw new NullReferenceException();
            AddOrUpdateProductDialog.Add(null, viewModel);
        };

        public static RoutedEventHandler OnRemoveProductClick { get; } = (sender, e) =>
        {
            var (viewModel, product) = GetData(sender);
            string message = $"Действительно удалить выбранный товар?";
            if (MessageBox.Show(message, "Удаление товара", MessageBoxButton.OKCancel) is MessageBoxResult.OK)
            {
                viewModel.RemoveProduct.Execute(product);
            }
        };

        public static RoutedEventHandler OnUpdateProductClick { get; } = (sender, e) =>
        {
            var (viewModel, product) = GetData(sender);
            AddOrUpdateProductDialog.Update(product, viewModel);
        };
        
        private static (IProductsViewModel viewModel, IProduct product) GetData(object sender)
        {
            FrameworkElement element = (FrameworkElement)sender;
            ContextMenu menu = element.FindAncestor<ContextMenu>() ?? throw new NullReferenceException();

            IProductsViewModel viewModel = menu.PlacementTarget.FindData<IProductsViewModel>() ?? throw new NullReferenceException();
            IProduct product = (IProduct)element.DataContext;

            return (viewModel, product);
        }
    }
}
