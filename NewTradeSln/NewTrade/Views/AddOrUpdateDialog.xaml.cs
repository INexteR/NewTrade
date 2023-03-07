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
    internal partial class AddOrUpdateProductDialog : AddOrUpdateDialogBase
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
    internal class AddOrUpdateDialogBase : AddOrUpdateDialogBase<ProductVM, IProductsViewModel>
    {
        public AddOrUpdateDialogBase(DialogMode mode, ProductVM entity, IProductsViewModel viewModel)
            : base(mode, entity, viewModel)
        {
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
    internal class AddOrUpdateDialogBase<TEntity, TViewModel> : Window
    {
        protected readonly AddOrUpdateDialogData<TEntity, TViewModel> dialogData;
        public AddOrUpdateDialogBase(DialogMode mode, TEntity entity, TViewModel viewModel)
        {
            ((IComponentConnector)this).InitializeComponent();
            dialogData = (AddOrUpdateDialogData<TEntity, TViewModel>)Resources[nameof(dialogData)];
            dialogData.Mode = mode;
            dialogData.Entity = entity;
            dialogData.ViewModel = viewModel;
        }

        public AddOrUpdateDialogBase()
        {
            dialogData = new();
        }
    }



    internal enum DialogMode
    {
        Add, Update
    }

    internal class AddOrUpdateProductDialogData : AddOrUpdateDialogData<ProductVM, IProductsViewModel>
    { }

    internal class AddOrUpdateDialogData<TEntity, TViewModel> : ViewModelBase
    {
        public DialogMode Mode { get; set; }
        public TempProduct? Product { get; set; }
        public IProductsViewModel? ProductViewModel { get; set; }
    }
}
