﻿global using System;
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
global using System.Windows.Controls.Primitives;
global using System.Windows.Markup;
global using System.Globalization;
global using System.ComponentModel;
global using System.Collections.ObjectModel;
global using System.Collections;
global using System.Runtime.CompilerServices;
global using System.Collections.Specialized;
global using Microsoft.EntityFrameworkCore;
global using NewTrade.Model.DTOs;
global using MVVM.ViewModels;
global using MVVM.Commands;
global using NewTrade.Model;
global using NewTrade.ViewModels;
using System.Reflection;

namespace NewTrade
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(new());
        }

        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Click += Help.OnButtonClick;
        }

        private void Button_Unloaded(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Click -= Help.OnButtonClick;
        }
    }
}
