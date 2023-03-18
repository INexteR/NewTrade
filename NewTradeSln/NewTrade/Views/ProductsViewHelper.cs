using Model;
using System;
using ViewModels;
using System.IO;
using System.Windows.Input;
using ShopViewModels;
using System.Windows.Data;
using System.Globalization;
using System.Linq;

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

        public static RoutedUICommand Add { get; } = new RoutedUICommand("Добавление", "Add", typeof(ProductsViewHelper));
        public static RoutedUICommand Remove { get; } = new RoutedUICommand("Добавление", "Remove", typeof(ProductsViewHelper));
        public static RoutedUICommand Update { get; } = new RoutedUICommand("Добавление", "Update", typeof(ProductsViewHelper));

        public static ExecutedRoutedEventHandler Executed { get; } = (s, e) =>
        {
            FrameworkElement view = (FrameworkElement)s;
            IProductsViewModel viewModel = (IProductsViewModel)view.DataContext;
            IProduct product = (IProduct)e.Parameter;

            MessageBox.Show($"view = \"{view}\"\r\nviewModel = \"{viewModel}\"\r\nтовар = \"{product?.Name}\"");
        };

        private static readonly TempProduct temp = new();

        public static CanExecuteRoutedEventHandler CanExecute { get; } = (s, e) =>
        {
            FrameworkElement view = (FrameworkElement)s;
            IProductsViewModel viewModel = (IProductsViewModel)view.DataContext;
            IProduct product = (IProduct)e.Parameter;

            if (viewModel is null)
            {
                return;
            }

            if (e.Command == Add)
                e.CanExecute = viewModel.AddProduct.CanExecute(product ?? temp);
            else if (e.Command == Remove)
                e.CanExecute = viewModel.RemoveProduct.CanExecute(product);
            else if (e.Command == Update)
                e.CanExecute = viewModel.UpdateProduct.CanExecute(product);
        };
    }
}
