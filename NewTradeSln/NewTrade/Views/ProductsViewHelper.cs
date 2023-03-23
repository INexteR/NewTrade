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
using System.Windows.Threading;
using System.Threading.Tasks;

namespace NewTrade.Views
{
    public static class ProductsViewHelper
    {
        public static RelayCommand OnAddProductClick { get; } = new RelayCommand<ICommandSource>(
            sender =>
            {
                FrameworkElement element = (FrameworkElement)sender;
                IProductsViewModel viewModel = element.FindData<IProductsViewModel>() ?? throw new NullReferenceException();
                AddOrUpdateProductDialog.Add(null, viewModel);
            },
            sender =>
            {
                FrameworkElement element = (FrameworkElement)sender;
                IProductsViewModel? viewModel = element.FindData<IProductsViewModel>();
                return viewModel?.CanAddAndUpdate ?? false;
            });

        public static RelayCommand OnRemoveProductClick { get; } = new RelayCommand<ICommandSource>(
            sender =>
            {
                var (viewModel, product) = GetData(sender);
                string message = $"Действительно удалить выбранный товар?";
                if (MessageBox.Show(message, "Удаление товара", MessageBoxButton.OKCancel, MessageBoxImage.Question)
                is MessageBoxResult.OK)
                {
                    viewModel?.RemoveProduct.Execute(product);
                    if (product.Path != null)
                        File.Delete(Path.Combine(ImageNameToPathConverter.ImageFolderPath, product.Path));
                }
            },
            sender =>
            {
                var (viewModel, product) = GetData(sender);
                return viewModel?.CanRemove ?? false;
            });

        public static RelayCommand OnUpdateProductClick { get; } = new RelayCommand<ICommandSource>(
            sender =>
            {
                var (viewModel, product) = GetData(sender);
                AddOrUpdateProductDialog.Update(product, viewModel!);
            },
            sender =>
            {
                var (viewModel, product) = GetData(sender);
                return viewModel?.CanAddAndUpdate ?? false;
            });
        public static RelayCommand OnCopyProductClick { get; } = new RelayCommand<ICommandSource>(
            sender =>
            {
                var (viewModel, product) = GetData(sender);
                AddOrUpdateProductDialog.Add(product, viewModel!);
            },
            sender =>
            {
                var (viewModel, product) = GetData(sender);
                return viewModel?.CanAddAndUpdate ?? false;
            });

        private static (IProductsViewModel? viewModel, IProduct product) GetData(object sender)
        {
            FrameworkElement element = (FrameworkElement)sender;
            ContextMenu? menu = element.FindAncestor<ContextMenu>();

            IProductsViewModel? viewModel = menu?.PlacementTarget.FindData<IProductsViewModel>();
            IProduct product = (IProduct)element.DataContext;

            return (viewModel, product);
        }

        public static RoutedEventHandler ContextMenuOpened { get; } = async (s, _)
            =>
        {
            await Task.Delay(100);
            CommandManager.InvalidateRequerySuggested();
            await ((DispatcherObject)s).Dispatcher.BeginInvoke(() =>
                         {
                             ContextMenu contextMenu = (ContextMenu)s;
                             if (!contextMenu.Items
                                    .OfType<UIElement>()
                                    .Any(elm => elm.Visibility == Visibility.Visible)
                             )
                             {
                                 contextMenu.IsOpen = false;
                                 SystemSounds.Beep.Play();
                             }
                         }); 
        };
    }
}
