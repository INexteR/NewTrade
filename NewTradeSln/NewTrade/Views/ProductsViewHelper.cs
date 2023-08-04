using Model;
using System;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Input;
using System.Windows.Markup;
using ViewModels;

namespace NewTrade.Views
{
    public static class ProductsViewHelper
    {
        public static RelayCommand RemoveProduct { get; } = new RelayCommand<FrameworkElement>(
            sender =>
            {
                var (viewModel, product) = GetData(sender);
                _ = product ?? throw new NullReferenceException();

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
                return viewModel.CanRemove;
            });

        public static RelayCommand UpdateProduct { get; } = new RelayCommand<FrameworkElement>(
            sender =>
            {
                var (viewModel, product) = GetData(sender);
                _ = product ?? throw new NullReferenceException();
                AddOrUpdateProductDialog.Update(product, viewModel!);
            },
            sender =>
            {
                var (viewModel, product) = GetData(sender);
                return viewModel.CanAddAndUpdate;
            });
        public static RelayCommand AddOrCopyProduct { get; } = new RelayCommand<FrameworkElement>(
            sender =>
            {
                var (viewModel, product) = GetData(sender);
                AddOrUpdateProductDialog.Add(product, viewModel!);
            },
            sender =>
            {
                var (viewModel, product) = GetData(sender);
                return viewModel.CanAddAndUpdate;
            });

        private static (IProductsViewModel viewModel, IProduct? product) GetData(FrameworkElement sender)
        {
            IProductsViewModel viewModel = ((Locator)sender.FindResource("locator")).Products ?? throw new NullReferenceException();
            IProduct? product = sender.DataContext as IProduct;

            return (viewModel, product);
        }

        public static RoutedEventHandler ContextMenuOpened { get; } = (s, _) =>
        {
            ContextMenu contextMenu = (ContextMenu)s;
            var gen = contextMenu.ItemContainerGenerator;
            int i;
            for (i = 0; i < gen.Items.Count; i++)
            {
                var item = (MenuItem)gen.ContainerFromIndex(i);
                if (item.Visibility == Visibility.Visible)
                {
                    break;
                }
            }
            if (i >= gen.Items.Count)
            {
                contextMenu.IsOpen = false;
                SystemSounds.Beep.Play();
            }
        };
    }


    /// <summary>Класс для урощения XAML.</summary>
    [MarkupExtensionReturnType(typeof(RoutedCommand))]
    public class ProductCommands : MarkupExtension
    {
        public static RoutedUICommand Add { get; } = new("Добавить товар", nameof(Add), typeof(ProductCommands));
        public static RoutedUICommand Remove { get; } = new("Удалить товар", nameof(Remove), typeof(ProductCommands));
        public static RoutedUICommand Update { get; } = new("Редактировать товар", nameof(Update), typeof(ProductCommands));

        public enum Commands { Add, Remove, Update }

        public Commands Command { get; set; }

        public ProductCommands()
            : this((Commands)(-1))
        { }

        public ProductCommands(Commands command)
        {
            Command = command;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            RoutedUICommand command = Command switch
            {
                Commands.Add => Add,
                Commands.Remove => Remove,
                Commands.Update => Update,
                _ => throw new NotImplementedException()
            };

            var provideValueTarget = (IProvideValueTarget?)serviceProvider.GetService(typeof(IProvideValueTarget))
                ?? throw new NullReferenceException();

            var target = (UIElement)provideValueTarget.TargetObject;
            var property = (DependencyProperty)provideValueTarget.TargetProperty;

            if (target.CommandBindings.Cast<CommandBinding>().All(cb => cb.Command != command))
            {
                CommandBinding cb = new(command, OnExecuted, OnCanExecute);
                target.CommandBindings.Add(cb);
            }
            return command;
        }

        private static void OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ICommand command = GetCommand(e.Command);
            command.TryExecute(e.Source);
        }

        private static void OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            ICommand command = GetCommand(e.Command);
            e.CanExecute = App.IsInDesignMode || command.CanExecute(e.Source);
        }

        private static ICommand GetCommand(ICommand e)
        {
            ICommand command;
            if (e == Add)
            {
                command = ProductsViewHelper.AddOrCopyProduct;
            }
            else if (e == Remove)
            {
                command = ProductsViewHelper.RemoveProduct;
            }
            else if (e == Update)
            {
                command = ProductsViewHelper.UpdateProduct;
            }
            else
            {
                throw new NotImplementedException();
            }
            return command;
        }
    }
}
