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
    internal partial class AddOrUpdateDialog : AddOrUpdateDialogBase
    {
        private AddOrUpdateDialog(DialogMode mode, ProductVM entity, IProductsViewModel viewModel) : base(mode, entity, viewModel)
        { }

        public static bool? Update(IProduct product, IProductsViewModel viewModel)
        {
            var window = new AddOrUpdateDialog(DialogMode.Update, product.Create<ProductVM>(), viewModel);
            return window.ShowDialog();
        }
        public static bool? Add(IProduct product, IProductsViewModel viewModel)
        {
            ProductVM entity = product is null ? new ProductVM() : product.Create<ProductVM>();
            var window = new AddOrUpdateDialog(DialogMode.Update, entity, viewModel);
            return window.ShowDialog();
        }
    }

    internal class AddOrUpdateDialogBase : AddOrUpdateDialogBase<ProductVM, IProductsViewModel>
    {
        public AddOrUpdateDialogBase(DialogMode mode, ProductVM entity, IProductsViewModel viewModel)
            : base(mode, entity, viewModel)
        { }
    }



    internal class ProductVM : ValidationBase/*, IProduct*/
    {
        //...
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
    }

    internal enum DialogMode
    {
        Add, Update
    }

    internal class AddOrUpdateDialogData : AddOrUpdateDialogData<ProductVM, IProductsViewModel>
    { }

    internal class AddOrUpdateDialogData<TEntity, TViewModel> : ViewModelBase
    {
        public DialogMode Mode { get => Get<DialogMode>(); set => Set(value); }
        public TEntity? Entity { get => Get<TEntity>(); set => Set(value); }
        public TViewModel? ViewModel { get => Get<TViewModel>(); set => Set(value); }
    }
}
