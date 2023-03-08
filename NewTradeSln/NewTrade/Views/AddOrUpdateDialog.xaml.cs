using Mapping;
using Model;
using ShopSQLite;
using ShopViewModels;
using System;
using System.ComponentModel;
using System.Windows.Markup;
using ViewModels;

namespace NewTrade.Views
{
    /// <summary>
    /// Логика взаимодействия для AddOrUpdateDialog.xaml
    /// </summary>
    internal partial class AddOrUpdateDialog : Window
    {
        private readonly AddOrUpdateDialogData dialogData;
        private AddOrUpdateDialog(DialogMode mode, IProduct product, IProductsViewModel productsViewModel)
        {
            dialogData = new(mode, product, productsViewModel);
            Resources[nameof(dialogData)] = dialogData;
            InitializeComponent();
        }

        public static bool? Add(IProductsViewModel productsViewModel)
        {
            var tempProduct = new TempProduct();
            var window = new AddOrUpdateDialog(DialogMode.Add, tempProduct, productsViewModel);
            return window.ShowDialog();
        }

        public static bool? Update(IProduct product, IProductsViewModel productsViewModel)
        {
            var window = new AddOrUpdateDialog(DialogMode.Update, product.Create<TempProduct>(), productsViewModel);
            return window.ShowDialog();
        }
    }

    internal enum DialogMode
    {
        Add, Update
    }

    internal class AddOrUpdateDialogData : ViewModelBase
    {
        public AddOrUpdateDialogData(DialogMode mode, IProduct product, IProductsViewModel productsViewModel)
        {
            Mode = mode;
            Product = product;
            ProductsViewModel = productsViewModel;
        }

        public DialogMode Mode { get; }
        public IProduct Product { get; }
        public IProductsViewModel ProductsViewModel { get; }
    }
}
