using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Data;
using System.ComponentModel;
using ShopModel.Entities;

namespace NewTrade
{
    public class ProductsViewSource : CollectionViewSource
    {
        public ProductsViewSource()
        {
            IsLiveFilteringRequested = true;
            IsLiveSortingRequested = true;
            LiveSortingProperties.Add(nameof(IProduct.Cost));
        }

        public ListSortDirection? SortDirection
        {
            get => (ListSortDirection?)GetValue(SortDirectionProperty);
            set => SetValue(SortDirectionProperty, value);
        }

        public static readonly DependencyProperty SortDirectionProperty =
            DependencyProperty.Register(nameof(SortDirection),
                typeof(ListSortDirection?),
                typeof(ProductsViewSource),
                new PropertyMetadata(OnDirectionChanged));

        private static void OnDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ProductsViewSource source = CheckSource(d);

            source.SortDescriptions.Clear();
            var direction = (ListSortDirection?)e.NewValue;
            if (direction.HasValue)
            {
                var description = new SortDescription(nameof(IProduct.Cost), direction.Value);
                source.SortDescriptions.Add(description);
            }
        }

        private static ProductsViewSource CheckSource(DependencyObject d)
        {
            if (d is not ProductsViewSource source)
                throw new NotImplementedException($"Реализовано только для {typeof(ProductsViewSource).FullName}.");
            return source;
        }

        public int ManufacturerId
        {
            get { return (int)GetValue(ManufacturerIdProperty); }
            set { SetValue(ManufacturerIdProperty, value); }
        }

        public static readonly DependencyProperty ManufacturerIdProperty =
            DependencyProperty.Register(nameof(ManufacturerId),
                typeof(int),
                typeof(ProductsViewSource),
                new PropertyMetadata((d, e) =>
                {
                    ProductsViewSource source = CheckSource(d);
                    source.manufacturer = (int)e.NewValue;
                    ChangeFilter(source);
                }));

        private int manufacturer;
        private FilterEventHandler? manufacturerFilter;

        private static void ChangeFilter(ProductsViewSource source)
        {
            source.Filter -= source.manufacturerFilter;
            if (source.manufacturer != -1)
            {
                source.manufacturerFilter = (o, e) =>
                {
                    var product = (IProduct)e.Item;
                    e.Accepted = product.ManufacturerId == source.manufacturer;
                };               
            }
            else
            {
                source.manufacturerFilter = null;
            }
            source.Filter += source.manufacturerFilter;
        }
    }
}
