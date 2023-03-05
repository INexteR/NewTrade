using Mapping;
using Model;
using ShopSQLite;
using ShopViewModels;
using System.Windows.Markup;
using ViewModels;

namespace NewTrade.Views
{
    /// <summary>
    /// Логика взаимодействия для AddOrUpdateDialog.xaml
    /// </summary>
    internal partial class AddOrUpdateProductDialog : AddOrUpdateProductDialogBase
    {
        private AddOrUpdateProductDialog(AddOrUpdateDialogMode mode, ProductVM entity, IProductsViewModel viewModel) : base(mode, entity, viewModel)
        { }

        public static void View(IProduct product, IProductsViewModel viewModel)
        {
            var window = new AddOrUpdateProductDialog(AddOrUpdateDialogMode.View, product.Create<ProductVM>(), viewModel);
            window.Show();
        }
        public static bool? Update(IProduct product, IProductsViewModel viewModel)
        {
            var window = new AddOrUpdateProductDialog(AddOrUpdateDialogMode.Update, product.Create<ProductVM>(), viewModel);
            return window.ShowDialog();
        }
        public static bool? Add(IProduct product, IProductsViewModel viewModel)
        {
            ProductVM entity;
            if (product is null)
            {
                entity = new ProductVM();
            }
            else
            {
                entity = product.Create<ProductVM>();
            }
            var window = new AddOrUpdateProductDialog(AddOrUpdateDialogMode.Update, entity, viewModel);
            return window.ShowDialog();
        }
    }

    internal class AddOrUpdateProductDialogBase : AddOrUpdateDialogBase<ProductVM, IProductsViewModel>
    {
        public AddOrUpdateProductDialogBase(AddOrUpdateDialogMode mode, ProductVM entity, IProductsViewModel viewModel)
            : base(mode, entity, viewModel)
        { }
    }



    // Если нужно, то можно сюда добавить валидацию свойств.
    // Тогда нужно использовать базовым классом ValidationBase.
    internal class ProductVM : ViewModelBase, IProduct
    {
        public int Id { get => Get<int>(); set => Set(value); }
        public string Name { get => Get<string>(); set => Set(value); }
        public int UnitId { get => Get<int>(); set => Set(value); }
        public decimal Cost { get => Get<decimal>(); set => Set(value); }
        public int ManufacturerId { get => Get<int>(); set => Set(value); }
        public int SupplierId { get => Get<int>(); set => Set(value); }
        public int CategoryId { get => Get<int>(); set => Set(value); }
        public int MaxDiscountAmount { get => Get<int>(); set => Set(value); }
        public sbyte? DiscountAmount { get => Get<sbyte>(); set => Set(value); }
        public int QuantityInStock { get => Get<int>(); set => Set(value); }
        public string Description { get => Get<string>(); set => Set(value); }
        public string? Path { get => Get<string>(); set => Set(value); }
        public IManufacturer Manufacturer { get => Get<IManufacturer>(); set => Set(value); }
    }
    internal class AddOrUpdateDialogBase<TEntity, TViewModel> : Window
    {
        protected readonly AddOrUpdateDialogData<TEntity, TViewModel> dialogData;
        public AddOrUpdateDialogBase(AddOrUpdateDialogMode mode, TEntity entity, TViewModel viewModel)
        {
            ((IComponentConnector)this).InitializeComponent();
            dialogData = (AddOrUpdateDialogData<TEntity, TViewModel>)Resources[nameof(dialogData)];
            dialogData.Mode = mode;
            dialogData.Entity = entity;
            dialogData.ViewModel = viewModel;
        }
    }

    internal enum AddOrUpdateDialogMode
    {
        View, Add, Update
    }

    internal class AddOrUpdateProductDialogData : AddOrUpdateDialogData<ProductVM, IProductsViewModel>
    {
        public AddOrUpdateProductDialogData()
        {
            if (Locator.IsInDesignMode)
            {
                Entity = new ProductVM() { Name = "Название времени разработки" };
                ViewModel = new ProductsViewModel(/* Модель времени разработки */ new Shop()) {/* Данные времени разработки */ };
            }
        }
    }

    internal class AddOrUpdateDialogData<TEntity, TViewModel> : ViewModelBase
    {
        public AddOrUpdateDialogMode Mode { get => Get<AddOrUpdateDialogMode>(); set => Set(value); }
        public TEntity? Entity { get => Get<TEntity>(); set => Set(value); }
        public TViewModel? ViewModel { get => Get<TViewModel>(); set => Set(value); }
    }
}
