using CommonNet6.Collection;
using Model;
using ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ViewModel;
using System.Windows;
using System.Windows.Input;

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
                    var id = e.OldItem?.Id ?? throw new ArgumentNullException("e.OldItem");
                    Products.FirstRemove(pr => pr.Id == id);
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

        public RelayCommand<IProduct> Remove => GetCommand<IProduct>(RemoveExecute, RemoveCanExecute);
        ICommand IProductsViewModel.Remove => Remove;

        private void RemoveExecute(IProduct product)
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

        private bool RemoveCanExecute(IProduct product)
        {
            return product != null;
        }
    }
}
