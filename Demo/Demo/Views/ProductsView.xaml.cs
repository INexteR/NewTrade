using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demo.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        private readonly ProductsVM productsVM;
        public ProductsView(User? user = null)
        {
            InitializeComponent();
            productsVM = new(user); 
            DataContext = productsVM;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Navigate(new AuthorizeView());
        }
    }
}
