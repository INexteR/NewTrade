using CommonNet6.Collection;
using Model;
using ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

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

        private void OnProductChanged(object sender, NotifyCollectionChangedAction<IProduct> e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    products.Add(e.NewItem ?? throw new ArgumentNullException("e.NewItem"));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    var id = e.OldItem?.Id ?? throw new ArgumentNullException("e.OldItem");
                    products.FirstRemove(pr => pr.Id == id);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    var index = products.IndexOf(e.OldItem ?? throw new ArgumentNullException("e.OldItem"));
                    products[index] = e.NewItem ?? throw new ArgumentNullException("e.NewItem");
                    break;
                default: throw new NotImplementedException();
            }
        }
        private void OnSourcesChanged(object? sender, EventArgs e)
        {
            if (_shop.IsSourcesLoaded)
            {
                Units = _shop.GetUnits();
                manufacturers.Reset(_shop.GetManufacturers());
                Suppliers = _shop.GetSuppliers();
                Categories = _shop.GetCategories();
                products.Reset(_shop.GetProducts());
            }
        }

        public string Name => _shop.Name;
      
        public RelayCommand<IProduct> AddProduct => GetCommand<IProduct>(AddProductExecute);
        public RelayCommand<IProduct> RemoveProduct => GetCommand<IProduct>(DeleteProductExecute);
        public RelayCommand<IProduct> UpdateProduct => GetCommand<IProduct>(UpdateProductExecute);

        private readonly ObservableCollection<IProduct> products = new();
        public IEnumerable<IProduct> Products => products;

        private readonly ObservableCollection<IManufacturer> manufacturers = new();
        public IEnumerable<IManufacturer> Manufacturers => manufacturers;

        public IEnumerable<ISupplier> Suppliers { get => Get<IEnumerable<ISupplier>>() ?? Array.Empty<ISupplier>(); private set => Set(value); }
        public IEnumerable<IUnit> Units { get => Get<IEnumerable<IUnit>>() ?? Array.Empty<IUnit>(); private set => Set(value); }
        public IEnumerable<ICategory> Categories { get => Get<IEnumerable<ICategory>>() ?? Array.Empty<ICategory>(); private set => Set(value); }

        private void AddProductExecute(IProduct product)
        {
            _shop.Add(product);
        }

        private void UpdateProductExecute(IProduct product)
        {
            _shop.Update(product);
        }

        private void DeleteProductExecute(IProduct product)
        {
            _shop.Delete(product);
        }

        public bool CanAdd => _shop.CanAdd;
        public bool CanUpdate => _shop.CanUpdate;
        public bool CanDelete => _shop.CanDelete;
    }
}
