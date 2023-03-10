global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using System.Windows;
global using System.Windows.Controls;
global using System.Windows.Data;
global using System.Windows.Documents;
global using System.Windows.Input;
global using System.Windows.Media;
global using System.Windows.Media.Imaging;
global using System.Windows.Navigation;
global using System.Windows.Shapes;
global using System.IO;
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using Microsoft.EntityFrameworkCore;
global using ShopSQLite;
global using ShopSQLite.Entities;
global using ShopSQLite.Initialization;
global using System.Collections.ObjectModel;
global using System.ComponentModel;
global using System.Runtime.CompilerServices;
global using System.Reflection;
global using Mapping;
global using Demo.Views;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BigViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = (BigViewModel)DataContext;
        }

        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            await Dispatcher.BeginInvoke(() => viewModel.Init());
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            if (newContent is AuthorizeView) Title = "Авторизация";
            if (newContent is ProductsView) Title = "Каталог";
        }
    }

    public static class WinHelper
    {
        public static void Navigate<TNewView>(this UserControl oldView) where TNewView : UserControl, new()
        {
            ((Window)oldView.Parent).Content = new TNewView();
        }
    }
}
