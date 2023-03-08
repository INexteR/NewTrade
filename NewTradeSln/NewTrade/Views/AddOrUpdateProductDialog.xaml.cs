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

        private AddOrUpdateProductDialog(DialogMode mode, ProductVM product, IProductsViewModel viewModel)
        {
            InitializeComponent();

            dialogData = (AddOrUpdateProductDialogData)Resources[nameof(dialogData)];
            dialogData.Mode = mode;
            dialogData.Product = product;
            dialogData.ProductsViewModel = viewModel;
            if (mode == DialogMode.Add)
                Title = "Добавление товара";
        }

        public static void Update(IProduct product, IProductsViewModel viewModel)
        {
            ProductVM copy = product?.Create<ProductVM>()
                ?? throw new ArgumentNullException(nameof(product));

            var window = new AddOrUpdateProductDialog(DialogMode.Update, copy, viewModel);
            window.ShowDialog();
        }
        public static void Add(IProduct? product, IProductsViewModel viewModel)
        {
            ProductVM copy = product is null ? new ProductVM()
                                             : product.Create<ProductVM>();

            var window = new AddOrUpdateProductDialog(DialogMode.Add, copy, viewModel);
            window.ShowDialog();
        }
    }

    internal class ProductVM : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitId { get; set; }
        public decimal Cost { get; set; }
        public int ManufacturerId { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
        public int MaxDiscountAmount { get; set; }
        public sbyte? DiscountAmount { get; set; }
        public int QuantityInStock { get; set; }
        public string Description { get; set; }
        public string? Path { get; set; }
        public IUnit Unit { get; set; }
        public IManufacturer Manufacturer { get; set; }
        public ISupplier Supplier { get; set; }
        public ICategory Category { get; set; }
        public IEnumerable<IOrderProduct> OrderProducts { get; }
    }

    internal enum DialogMode
    {
        Add, Update
    }

    internal class AddOrUpdateProductDialogData : ViewModelBase
    {
        public DialogMode Mode { get => Get<DialogMode>(); set => Set(value); }
        public ProductVM? Product { get => Get<ProductVM>(); set => Set(value); }
        public IProductsViewModel? ProductsViewModel { get => Get<IProductsViewModel>(); set => Set(value); }
    }
}
