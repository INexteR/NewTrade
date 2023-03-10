namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BigViewModel viewModel = BigViewModel.Instance;
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            await Dispatcher.BeginInvoke(() => viewModel.Init());
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            Title = newContent switch
            {
                AuthorizeView => "Авторизация",
                ProductsView => "Каталог",
                _ => throw new NotSupportedException()
            };
        }

    }

    public static class WinHelper
    {
        public static void Navigate<TContent>(this DependencyObject oldView)
            where TContent : new()
        {
            oldView.Navigate(new TContent());
        }
        public static void Navigate(this DependencyObject oldView, object content)
        {
            Window.GetWindow(oldView).Content = content;
        }
    }
}
