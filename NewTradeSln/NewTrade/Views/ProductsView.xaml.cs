﻿using System.Windows.Controls.Primitives;
using System.Collections.Specialized;
using System.Windows.Input;

namespace NewTrade.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductsViewModel.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        private readonly ItemContainerGenerator generator;
        public ProductsView()
        {
            InitializeComponent();
            generator = list.ItemContainerGenerator;
            generator.ItemsChanged += Generator_ItemsChanged;
        }

        private void Generator_ItemsChanged(object sender, ItemsChangedEventArgs e)
        {
            if (e.Action is NotifyCollectionChangedAction.Add)
            {
                var container = (ListBoxItem)generator.ContainerFromIndex(e.Position.Index);
                container.BringIntoView();
            }
        }
    }
}
