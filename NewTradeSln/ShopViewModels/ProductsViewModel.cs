using CommonNet6.Collection;
using Model;
using MVVM.ViewModels;
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
            //загрузка пока что в конструкторе, это плохо, надо как-то решать
            //Products = new ObservableCollection<IProduct>(_shop.GetProducts());
            manufacturers = new(_shop.GetManufacturers().ToArray());
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
                    var articleNumber = e.OldItem?.ArticleNumber ?? throw new ArgumentNullException("e.OldItem.ArticleNumber");
                    Products.FirstRemove(pr => string.Equals(pr.ArticleNumber, articleNumber));
                    break;
                case NotifyCollectionChangedAction.Replace:
                    var index = Products.IndexOf(e.OldItem ?? throw new ArgumentNullException("e.OldItem"));
                    Products[index] = e.NewItem ?? throw new ArgumentNullException("e.NewItem");
                    break;
            }
        }

        public string Name => _shop.Name;

        public ObservableCollection<IProduct> Products { get; } = new();

        private readonly ReadOnlyCollection<IManufacturer> manufacturers;
        public IReadOnlyCollection<IManufacturer> Manufacturers => manufacturers;
        public IProduct? SelectedProduct { get => Get<IProduct?>(); set => Set(value); }
    }
}
