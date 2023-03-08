using Model;
using NewTrade.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ViewModels;

namespace NewTrade
{
    public static class ViewsHelper
    {
        public static ReadOnlyDictionary<string, ListSortDirection?> Directions { get; }
            = new Dictionary<string, ListSortDirection?>
            {
                { "По умолчанию", null },
                { "По возрастанию", ListSortDirection.Ascending },
                { "По убыванию", ListSortDirection.Descending }
            }.AsReadOnly();//ну да, здесь нужен DictionaryConverter

        public static object AllManufacturers { get; } = new { Id = -1, Name = "Все производители" };

        public static KeyEventHandler OnlyDigits { get; } = (s, e) =>
        {
            if (e.Key is not (>= Key.D0 and <= Key.D9 or
            >= Key.NumPad0 and <= Key.NumPad9 or Key.Back))
            {
                e.Handled = true;
            }
        };

        public static RoutedEventHandler OpenDialogToAdd { get; } = (s, e) =>       
            AddOrUpdateDialog.Add((IProductsViewModel)((Button)s).DataContext);

        public static RoutedEventHandler OpenDialogToUpdate { get; } = (s, e) =>       
            AddOrUpdateDialog.Update((IProduct)((MenuItem)s).DataContext, GetProductsViewModel(s));
        
        public static RoutedEventHandler RemoveProductConfirmation { get; } = (s, e) =>
        {
            var product = (IProduct)((MenuItem)s).DataContext;
            var productsViewModel = GetProductsViewModel(s);
            if (MessageBox.Show("Действительно удалить выбранный товар?", "Подтверждение", 
                MessageBoxButton.OKCancel, MessageBoxImage.Question) is MessageBoxResult.OK)
            {
                try
                {
                    productsViewModel.RemoveProduct.Execute(product);
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Не удалось удалить выбранный товар.", default, MessageBoxImage.Error);
                }
            }
        };

        private static IProductsViewModel GetProductsViewModel(object sender)
        {
            var element = (FrameworkElement)sender;
            var parent = (FrameworkElement)element.Parent;
            return (IProductsViewModel)parent.Tag;
        }
    }
}
