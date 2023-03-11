namespace Demo.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        private readonly BigViewModel viewModel = BigViewModel.Instance;
        private readonly CollectionViewSource source = null!;

        public ProductsView()
        {
            InitializeComponent();

            source = (CollectionViewSource)Resources["productsView"];
            sort.SelectionChanged += Sort_SelectionChanged;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Exit();
            this.Navigate<AuthorizeView>();
        }

        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            source.SortDescriptions.Clear();
            var direction = (ListSortDirection?)sort.SelectedValue;
            if (direction.HasValue)
            {
                source.SortDescriptions.Add(new("Cost", direction.Value));
            }
        }

        private string? searchText;
        private object searchManufacturer =  ViewsHelper.AllManufacturers;
        public string? SearchText
        {
            get => searchText;
            set { searchText = value; SearchChanged(value?.Trim(), SearchManufacturer as Manufacturer); }
        }
        public object SearchManufacturer
        {
            get => searchManufacturer;
            set { searchManufacturer = value ?? ViewsHelper.AllManufacturers; SearchChanged(SearchText?.Trim(), value as Manufacturer); }
        }

        private FilterEventHandler? filter;
        private void SearchChanged(string? text, Manufacturer? manufacturer)
        {
            source.Filter -= filter;

            filter = (string.IsNullOrEmpty(text), manufacturer == null) switch
            {
                (false, false) => (s, e) => NameManufacturerSearch(s, e, text!, manufacturer!),
                (false, true) => (s, e) => NameSearch(s, e, text!),
                (true, false) => (s, e) => ManufacturerSearch(s, e, manufacturer!),
                _ => null,
            };

            source.Filter += filter;
        }

        private static void NameSearch(object sender, FilterEventArgs e, string name)
        {
            e.Accepted = ((Product)e.Item).Name.Contains(name);
        }
        private static void ManufacturerSearch(object sender, FilterEventArgs e, Manufacturer manufacturer)
        {
            e.Accepted = ((Product)e.Item).Manufacturer == manufacturer;
        }
        private static void NameManufacturerSearch(object sender, FilterEventArgs e, string name, Manufacturer manufacturer)
        {
            Product product = (Product)e.Item;
            e.Accepted = product.Manufacturer == manufacturer &&
                         product.Name.Contains(name);
        }
    }
}
