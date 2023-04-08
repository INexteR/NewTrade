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

        private string? inputText;
        private object selectedManufacturer = ViewsHelper.AllManufacturers;
        public string? SearchText
        {
            get => inputText;
            set { inputText = value; SearchChanged(); }
        }
        public object SearchManufacturer
        {
            get => selectedManufacturer;
            set { selectedManufacturer = value ?? ViewsHelper.AllManufacturers; SearchChanged(); }
        }

        private FilterEventHandler? filter;
        private void SearchChanged()
        {
            string searchText = inputText?.Trim() ?? string.Empty;
            Manufacturer? searchManufacturer = selectedManufacturer as Manufacturer;
            source.Filter -= filter;

            filter = (string.IsNullOrEmpty(searchText), searchManufacturer == null) switch
            {
                (false, false) => (object sender, FilterEventArgs e) =>
                {
                    Product product = (Product)e.Item;
                    e.Accepted = product.Manufacturer == searchManufacturer &&
                                 product.Name.Contains(searchText);
                }
                ,
                (false, true) => (object sender, FilterEventArgs e) => e.Accepted = ((Product)e.Item).Name.Contains(searchText),
                (true, false) => (object sender, FilterEventArgs e) => e.Accepted = ((Product)e.Item).Manufacturer == searchManufacturer,
                _ => null,
            };

            source.Filter += filter;
        }

        private void OnAddProductDialog(object sender, RoutedEventArgs e)
        {
            var dialog = new AddOrUpdateProductDialog();
            dialog.Owner = Window.GetWindow(this);
            dialog.Title = "Добавление нового товара";
            dialog.ShowDialog();
        }
        private void OnUpdateProductDialog(object sender, RoutedEventArgs e)
        {
            var dialog = new AddOrUpdateProductDialog();
            dialog.Owner = Window.GetWindow(this);
            dialog.Title = "Редактирование товара";
            Product product = (Product)((Button)sender).CommandParameter;
            dialog.Resources["product"] = product.Clone();
            dialog.ShowDialog();
        }
    }

    public static class CloneHelper
    {
        public static Product Clone(this Product product) 
        {
            return new Product()
            {
                // Здесь копирование нужных свойств
                Id = product.Id,
                Name = product.Name,
                ManufacturerId = product.ManufacturerId,
            };
        }
    }
}
