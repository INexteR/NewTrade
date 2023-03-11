using Mapping;
using Model;
using ShopSQLite;
using ShopViewModels;
using System;
using System.Collections.Generic;
using ViewModels;

namespace NewTrade.Views
{
    /// <summary>
    /// Логика взаимодействия для AddOrUpdateDialog.xaml
    /// </summary>
    internal partial class AddOrUpdateProductDialog : Window
    {
        private readonly AddOrUpdateProductDialogData dialogData;

        private AddOrUpdateProductDialog(DialogMode mode, TempProduct product, IProductsViewModel viewModel)
        {
            InitializeComponent();

            dialogData = (AddOrUpdateProductDialogData)Resources[nameof(dialogData)];
            dialogData.Mode = mode;
            dialogData.Product = product;
            dialogData.ProductsViewModel = viewModel;
            if (mode is DialogMode.Add)
                Title = "Добавление товара";
        }

        public static void Update(IProduct product, IProductsViewModel viewModel)
        {
            TempProduct copy = product?.Create<TempProduct>()
                ?? throw new ArgumentNullException(nameof(product));

            var window = new AddOrUpdateProductDialog(DialogMode.Update, copy, viewModel);
            window.ShowDialog();
        }
        public static void Add(IProduct? product, IProductsViewModel viewModel)
        {
            TempProduct copy = product is null ? new TempProduct()
                                             : product.Create<TempProduct>();

            var window = new AddOrUpdateProductDialog(DialogMode.Add, copy, viewModel);
            window.ShowDialog();
        }
    }

    internal enum DialogMode
    {
        Add, Update
    }

    internal class AddOrUpdateProductDialogData : ViewModelBase
    {
        public DialogMode Mode { get => Get<DialogMode>(); set => Set(value); }
        public TempProduct? Product { get => Get<TempProduct>(); set => Set(value); }
        public IProductsViewModel? ProductsViewModel { get => Get<IProductsViewModel>(); set => Set(value); }
    }
}
