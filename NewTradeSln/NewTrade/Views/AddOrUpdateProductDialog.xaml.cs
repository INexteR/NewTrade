using Mapping;
using Model;
using ShopSQLite;
using ShopViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Input;
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
        }

        public static void Update(IProduct product, IProductsViewModel viewModel)
        {
            TempProduct copy = product?.Create<TempProduct>()
                ?? throw new ArgumentNullException(nameof(product));

            var window = new AddOrUpdateProductDialog(DialogMode.Update, copy, viewModel)
            {
                Title = "Редактирование товара",
            };
            window.button.Command = viewModel.UpdateProduct;
            window.ShowDialog();
        }
        public static void Add(IProduct? product, IProductsViewModel viewModel)
        {
            TempProduct copy = product is null ? new TempProduct()
                                             : product.Create<TempProduct>();

            var window = new AddOrUpdateProductDialog(DialogMode.Add, copy, viewModel) 
            { 
                Title = "Добавление товара"
            };
            window.button.Command = viewModel.AddProduct;
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
