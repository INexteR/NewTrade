using Mapping;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Markup;
using ViewModels;

namespace NewTrade.Views
{
    /// <summary>
    /// Логика взаимодействия для AddOrUpdateDialog.xaml
    /// </summary>
    internal partial class AddOrUpdateProductDialog : AddOrUpdateProductDialogBase
    {
        private AddOrUpdateProductDialog(DialogMode mode, ProductVM entity, IProductsViewModel viewModel) 
            : base(mode, entity, viewModel)
        { }

        public static bool? Update(AddOrUpdateProductDialogArgs args)
        {
            var window = new AddOrUpdateProductDialog(DialogMode.Update, args.Product?.Create<ProductVM>() ?? throw new ArgumentNullException("args.Product"), args.ViewModel);
            return window.ShowDialog();
        }
        public static bool? Add(AddOrUpdateProductDialogArgs args)
        {
            ProductVM entity = args.Product is null ? new ProductVM() : args.Product.Create<ProductVM>();
            var window = new AddOrUpdateProductDialog(DialogMode.Add, entity, args.ViewModel);
            return window.ShowDialog();
        }
    }


    internal partial class AddOrUpdateProductDialogArgs
    {
        public IProduct? Product { get; }
        public IProductsViewModel ViewModel { get; }

        public AddOrUpdateProductDialogArgs(IProduct? entity, IProductsViewModel viewModel)
        {
            Product = entity;
            ViewModel = viewModel;
        }
    }

    internal class ProductVM : ValidationBase, IProduct
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
    internal class AddOrUpdateProductDialogBase : Window
    {
        protected readonly AddOrUpdateProductDialogData dialogData;
        public AddOrUpdateProductDialogBase(DialogMode mode, ProductVM product, IProductsViewModel viewModel)
        {
            ((IComponentConnector)this).InitializeComponent();
            dialogData = (AddOrUpdateProductDialogData) Resources[nameof(dialogData)];
            dialogData.Mode = mode;
            dialogData.Product = product;
            dialogData.ProductsViewModel = viewModel;
        }

        public AddOrUpdateProductDialogBase()
        {
            dialogData = new();
        }
    }



    internal enum DialogMode
    {
        Add, Update
    }

    internal class AddOrUpdateProductDialogData : ViewModelBase
    {
        public DialogMode Mode { get => Get<DialogMode>(); set=>Set(value); }
        public ProductVM Product { get => Get<ProductVM>(); set => Set(value); }
        public IProductsViewModel? ProductsViewModel { get => Get<IProductsViewModel>(); set => Set(value); }
    }
}
