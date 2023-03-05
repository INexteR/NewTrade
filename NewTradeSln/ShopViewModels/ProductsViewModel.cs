using CommonNet6.Collection;
using Model;
using ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

namespace ShopViewModels
{
    public class ProductsViewModel : ViewModelBase, IProductsViewModel
    {
        private readonly IShop _shop;

        public ProductsViewModel(IShop shop)
        {
            _shop = shop;
            _shop.ProductChanged += OnProductChanged;
            _shop.SourcesLoadedChanged += OnSourcesChanged;
        }

        private void OnProductChanged(object sender, NotifyListChangedEventArgs<IProduct> e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                    Products.Reset(_shop.GetProducts());
                    break;
                case NotifyCollectionChangedAction.Add:
                    Products.Add(e.NewItem ?? throw new ArgumentNullException("e.NewItem"));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var id = e.OldItem?.Id ?? throw new ArgumentNullException("e.OldItem");
                    Products.FirstRemove(pr => pr.Id == id);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    var index = Products.IndexOf(e.OldItem ?? throw new ArgumentNullException("e.OldItem"));
                    Products[index] = e.NewItem ?? throw new ArgumentNullException("e.NewItem");
                    break;
            }
        }
        private void OnSourcesChanged(object? sender, EventArgs e)
        {
            if (_shop.IsSourcesLoaded)
            {
                Units = _shop.GetUnits();
                Manufacturers = _shop.GetManufacturers();
                Suppliers = _shop.GetSuppliers();
                Categories = _shop.GetCategories();
            }
            else
            {
                Units = Array.Empty<IUnit>();
                Manufacturers = Array.Empty<IManufacturer>();
                Suppliers = Array.Empty<ISupplier>();
                Categories = Array.Empty<ICategory>();
            }
        }

        public string Name => _shop.Name;

        public ObservableCollection<IProduct> Products { get; } = new();
        IEnumerable<IProduct> IProductsViewModel.Products => Products;


        public RelayCommand<IProduct> AddProduct => GetCommand<IProduct>(AddProductExecute);

        private void AddProductExecute(IProduct parameter)
        {
            throw new NotImplementedException();
        }

        public RelayCommand<IProduct> RemoveProduct => GetCommand<IProduct>(RemoveProductExecute, RemoveProductCanExecute);

        public RelayCommand<IProduct> ChangeProduct => GetCommand<IProduct>(ChangeProductExecute);

        public IEnumerable<IManufacturer> Manufacturers { get => Get<IEnumerable<IManufacturer>>() ?? Array.Empty<IManufacturer>(); private set => Set(value); }
        public IEnumerable<ISupplier> Suppliers { get => Get<IEnumerable<ISupplier>>() ?? Array.Empty<ISupplier>(); private set => Set(value); }
        public IEnumerable<IUnit> Units { get => Get<IEnumerable<IUnit>>() ?? Array.Empty<IUnit>(); private set => Set(value); }
        public IEnumerable<ICategory> Categories { get => Get<IEnumerable<ICategory>>() ?? Array.Empty<ICategory>(); private set => Set(value); }

        private void ChangeProductExecute(IProduct parameter)
        {
            throw new NotImplementedException();
        }

        private void RemoveProductExecute(IProduct product)
        {
            try
            {
                _shop.Remove(product);
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private bool RemoveProductCanExecute(IProduct product)
        {
            return product != null;
        }
    }
}
