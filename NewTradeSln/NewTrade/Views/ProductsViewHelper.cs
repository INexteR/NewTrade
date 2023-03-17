using Model;
using System;
using ViewModels;
using System.IO;

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

        public static RoutedEventHandler OnDeleteProductClick { get; } = (sender, e) =>
        {
            var (viewModel, product) = GetData(sender);
            string message = $"Действительно удалить выбранный товар?";
            if (MessageBox.Show(message, "Удаление товара", MessageBoxButton.OKCancel, MessageBoxImage.Question) 
            is MessageBoxResult.OK)
            {
                viewModel.RemoveProduct.Execute(product);
                if (product.Path != null)
                File.Delete(Path.Combine(ImageNameToPathConverter.ImageFolderPath, product.Path));
            }
        };

        public static RoutedEventHandler OnUpdateProductClick { get; } = (sender, e) =>
        {
            var (viewModel, product) = GetData(sender);
            AddOrUpdateProductDialog.Update(product, viewModel);
        };
        public static RoutedEventHandler OnCopyProductClick { get; } = (sender, e) =>
        {
            var (viewModel, product) = GetData(sender);
            AddOrUpdateProductDialog.Add(product, viewModel);
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
