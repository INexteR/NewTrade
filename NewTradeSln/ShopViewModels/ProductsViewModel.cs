using CommonNet6.Collection;
using Model;
using ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ViewModel;

namespace ShopViewModels
{
    public class ProductsViewModel : ViewModelBase, IProductsViewModel
    {
        private readonly IShop _shop;

        public ProductsViewModel(IShop shop)
        {
            _shop = shop;
            _shop.ProductChanged += OnProductChanged;
            _shop.ManufacturersChanged += OnManufacturersChanged;
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
                    var articleNumber = e.OldItem?.Id ?? throw new ArgumentNullException("e.OldItem");
                    Products.FirstRemove(pr => string.Equals(pr.Id, articleNumber));
                    break;
                case NotifyCollectionChangedAction.Replace:
                    var index = Products.IndexOf(e.OldItem ?? throw new ArgumentNullException("e.OldItem"));
                    Products[index] = e.NewItem ?? throw new ArgumentNullException("e.NewItem");
                    break;
            }
        }
        private void OnManufacturersChanged(object? sender, EventArgs e)
        {
            Manufacturers.Reset(_shop.GetManufacturers());          
        }

        public string Name => _shop.Name;

        public ObservableCollection<IProduct> Products { get; } = new();

        public ObservableCollection<IManufacturer> Manufacturers { get; } = new();

        public IProduct? SelectedProduct { get => Get<IProduct?>(); set => Set(value); }
    }
}
