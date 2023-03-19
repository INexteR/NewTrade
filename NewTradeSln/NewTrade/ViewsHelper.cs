using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;
using System.Windows.Media;
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
            >= Key.NumPad0 and <= Key.NumPad9 or Key.Back or Key.Left or Key.Right))
            {
                e.Handled = true;
            }
        };

        public static TextCompositionEventHandler OnlyNumbers { get; } = (s, e) =>
        {
            var textBox = (TextBox)s;
            string fullText;
            // Если TextBox содержит выделенный текст, то заменяем его на e.Text
            if (textBox.SelectionLength > 0)
            {
                fullText = textBox.Text.Replace(textBox.SelectedText, e.Text);
            }
            else
            {   // Иначе нам нужно вставить новый текст в позицию курсора
                fullText = textBox.Text.Insert(textBox.CaretIndex, e.Text);
            }
            e.Handled = !decimal.TryParse(fullText, CultureInfo.InvariantCulture, out var _);
        };

        public static T? FindAncestor<T>(this DependencyObject dobj)
            where T : DependencyObject
        {
            while (dobj is not null)
            {
                if (dobj is T t)
                    return t;

                dobj = VisualTreeHelper.GetParent(dobj);
            }
            return null;
        }

        public static FrameworkElement? FindDataAncestor<TData>(this DependencyObject dobj)
        {
            while (dobj is not null)
            {
                if (dobj is FrameworkElement element and { DataContext: TData })
                    return element;

                dobj = VisualTreeHelper.GetParent(dobj);
            }
            return null;
        }

        public static TData? FindData<TData>(this DependencyObject dobj)
        {
            while (dobj is not null)
            {
                if (dobj is FrameworkElement and { DataContext: TData data })
                    return data;

                dobj = VisualTreeHelper.GetParent(dobj);
            }
            return default;
        }

        public static RoutedEventHandler CloseWindow { get; } = (_, e) => 
        Window.GetWindow((DependencyObject)e.Source).Close();

        public static void Msg(this string txt, string? title = null, MessageBoxButton btn = default, MessageBoxImage img = MessageBoxImage.Error)
        {
            MessageBox.Show(txt, title, btn, img);
        }

        public static RoutedEventHandler LayoutSetup { get; } = (s, e) =>
        {
            var element = (FrameworkElement)s;
            var viewModel = (IProductsViewModel)element.DataContext;
            var contextMenu = (ContextMenu)element.Resources["contextMenu"];
            var pnl = (Panel)element.FindName("pnl");
            if (viewModel.CanRemove)
                return;
            if (viewModel.CanAddAndUpdate)
                contextMenu.Items.RemoveAt(2);
            else
            {
                pnl.Children.Remove(pnl.Children[^1]);
                element.Resources.Remove(nameof(contextMenu));
            }
        };
    }
}
