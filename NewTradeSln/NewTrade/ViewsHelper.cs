using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            >= Key.NumPad0 and <= Key.NumPad9 or Key.Back))
            {
                e.Handled = true;
            }
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

        public static RoutedEventHandler CloseWindow { get; } = (s, _) => Window.GetWindow((DependencyObject)s).Close();
    }
}
