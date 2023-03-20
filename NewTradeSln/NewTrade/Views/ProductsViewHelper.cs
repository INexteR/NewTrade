using Model;
using System;
using ViewModels;
using System.IO;
using System.Windows.Input;
using ShopViewModels;
using System.Windows.Data;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Runtime.Intrinsics.X86;
using System.Media;

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

        /// <summary>Какое нужно действие - передаётся через параметр команды.</summary>
        public static RoutedUICommand General { get; } = new RoutedUICommand("Общая команда", nameof(General), typeof(ProductsViewHelper));

        public static ExecutedRoutedEventHandler Executed { get; } = (s, e) =>
        {
            FrameworkElement view = (FrameworkElement)s;
            IProductsViewModel viewModel = (IProductsViewModel)view.DataContext;
            FrameworkElement source = (FrameworkElement)e.OriginalSource;
            IProduct? product = source.DataContext as IProduct;
            string action = e.Parameter?.ToString() ?? string.Empty;

            MessageBox.Show($"view = \"{view}\"\r\nviewModel = \"{viewModel}\r\nsource = \"{source}\"\r\nтовар = \"{product?.Name}\"\r\nдействие = \"{action}");
        };

        private static readonly TempProduct temp = new();

        public static CanExecuteRoutedEventHandler CanExecute { get; } = (s, e) =>
        {
            FrameworkElement view = (FrameworkElement)s;
            IProductsViewModel viewModel = (IProductsViewModel)view.DataContext;
            FrameworkElement source = (FrameworkElement)e.OriginalSource;
            IProduct? product = source.DataContext as IProduct;
            string commandName = e.Parameter?.ToString() ?? string.Empty;

            if (viewModel is null)
            {
                return;
            }

            e.CanExecute = IsAccessibleCommand(viewModel, commandName);
        };

        public static bool IsAccessibleCommand(IProductsViewModel viewModel, string commandName)
        {
            return commandName?.ToUpper() switch
            {
                "ADD" => viewModel.AddProduct.CanExecute(temp),
                "REMOVE" => viewModel.RemoveProduct.CanExecute(temp),
                "UPDATE" => viewModel.UpdateProduct.CanExecute(temp),
                _ => false
            };

        }

        public static RoutedEventHandler ContextMenuOpened { get; } = (s, _) =>
        {
            ContextMenu contextMenu = (ContextMenu)s;
            if(contextMenu.ActualHeight == 0)
            {
                contextMenu.IsOpen = false;
                SystemSounds.Beep.Play();
            }
        };
    }
}
