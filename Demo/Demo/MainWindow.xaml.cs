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
global using System.ComponentModel.DataAnnotations;
global using System.ComponentModel.DataAnnotations.Schema;
global using Microsoft.EntityFrameworkCore;
global using ShopSQLite.Entities;
global using ShopSQLite.Initialization;
global using System.Collections.ObjectModel;
global using System.ComponentModel;
global using System.Reflection;
global using Mapping;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
