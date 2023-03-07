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
    internal partial class AddOrUpdateDialog : Window
    {
        private readonly AddOrUpdateDialogData dialogData;
        private AddOrUpdateDialog(DialogMode mode, TempProduct product, IProductsViewModel productsViewModel)
        {
            InitializeComponent();
            dialogData = (AddOrUpdateDialogData)Resources[nameof(dialogData)];
            dialogData.Mode = mode;
            dialogData.Product = product;
            dialogData.ProductViewModel = productsViewModel;
        }

        public static bool? Update(IProduct product, IProductsViewModel productsViewModel)
        {
            var window = new AddOrUpdateDialog(DialogMode.Update, product.Create<TempProduct>(), productsViewModel);
            return window.ShowDialog();
        }
        public static bool? Add(IProduct? product, IProductsViewModel productsViewModel)
        {
            TempProduct tempProduct = product is null ? new TempProduct() : product.Create<TempProduct>();
            var window = new AddOrUpdateDialog(DialogMode.Update, tempProduct, productsViewModel);
            return window.ShowDialog();
        }
    }

    internal enum DialogMode
    {
        Add, Update
    }

    internal class AddOrUpdateDialogData
    {
        public DialogMode Mode { get; set; }
        public TempProduct? Product { get; set; }
        public IProductsViewModel? ProductViewModel { get; set; }
    }
}
