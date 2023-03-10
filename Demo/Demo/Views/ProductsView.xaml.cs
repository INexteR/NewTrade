namespace Demo.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        private BigViewModel viewModel = null!;
        private CollectionViewSource source = null!;
        public ProductsView()
        {
            InitializeComponent();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Exit();
            this.Navigate<AuthorizeView>();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel = (BigViewModel)DataContext;
            source = (CollectionViewSource)Resources["productsView"];
            sort.SelectionChanged += Sort_SelectionChanged;
            filter.SelectionChanged += Filter_SelectionChanged;
            search.TextChanged += Search_TextChanged;
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

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            source.Filter -= ftr;
            if (!string.IsNullOrWhiteSpace(search.Text))
            {
                if (IsFilterNotContains(NameSearch))
                    ftr += NameSearch;
            }
            else
                ftr -= NameSearch;
            source.Filter += ftr;
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            source.Filter -= ftr;
            if (filter.SelectedIndex != 0)
            {
                if (IsFilterNotContains(ManufacturerFilter))
                    ftr += ManufacturerFilter;
            }
            else
                ftr -= ManufacturerFilter;
            source.Filter += ftr;
        }

        private bool IsFilterNotContains(FilterEventHandler filter)
        {
            return ftr is null || !ftr.GetInvocationList().Contains(filter);
        }

        private FilterEventHandler? ftr;

        private void NameSearch(object sender, FilterEventArgs e)
        {
            if (!((Product)e.Item).Name.Contains(search.Text))
                e.Accepted = false;
        }
        private void ManufacturerFilter(object sender, FilterEventArgs e)
        {
            if (((Product)e.Item).Manufacturer != Manufacturer)
                e.Accepted = false;
        }

        private Manufacturer? Manufacturer => filter.SelectedItem as Manufacturer;
    }
}
