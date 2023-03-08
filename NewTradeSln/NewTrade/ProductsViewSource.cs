using System;
using System.Windows.Data;
using System.ComponentModel;
using Model;
using System.Collections.Generic;

namespace NewTrade
{
    public class ProductsViewSource : CollectionViewSource
    {
        public ProductsViewSource()
        {
            IsLiveFilteringRequested = true;
            IsLiveSortingRequested = true;
            LiveSortingProperties.Add(nameof(IProduct.Cost));
            LiveFilteringProperties.Add(nameof(IProduct.Name));
            LiveFilteringProperties.Add(nameof(IProduct.Description));
            LiveFilteringProperties.Add(nameof(IProduct.Manufacturer));
            LiveFilteringProperties.Add(nameof(IProduct.ManufacturerId));
            LiveFilteringProperties.Add(nameof(IProduct.Cost));
            LiveFilteringProperties.Add(nameof(IProduct.QuantityInStock));
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
            get => (int)GetValue(ManufacturerIdProperty);
            set => SetValue(ManufacturerIdProperty, value);
        }

        public static readonly DependencyProperty ManufacturerIdProperty =
            DependencyProperty.Register(nameof(ManufacturerId),
                typeof(int),
                typeof(ProductsViewSource),
                new PropertyMetadata(static (d, e) =>
                {
                    ProductsViewSource source = CheckSource(d);
                    if (source.ManufacturerId != -1)
                        source.filters.Add(source.ManufacturerFilter);
                    else
                        source.filters.Remove(source.ManufacturerFilter);
                    source.Filter -= source.InternalFilter;
                    source.Filter += source.InternalFilter;
                }));

        public string ProductName
        {
            get => (string)GetValue(ProductNameProperty);
            set => SetValue(ProductNameProperty, value);
        }

        public static readonly DependencyProperty ProductNameProperty =
            DependencyProperty.Register(nameof(ProductName),
                typeof(string),
                typeof(ProductsViewSource),
                new PropertyMetadata(string.Empty,
                    static (d, e) =>
                    {
                        ProductsViewSource source = CheckSource(d);
                        if (source.ProductName != string.Empty)
                            source.filters.Add(source.ProductNameSearch);
                        else
                            source.filters.Remove(source.ProductNameSearch);
                        source.Filter -= source.InternalFilter;
                        source.Filter += source.InternalFilter;
                    }, TrimString));



        public string ProductDescription
        {
            get => (string)GetValue(ProductDescriptionProperty);
            set => SetValue(ProductDescriptionProperty, value);
        }

        public static readonly DependencyProperty ProductDescriptionProperty =
            DependencyProperty.Register(nameof(ProductDescription),
                typeof(string),
                typeof(ProductsViewSource),
                new PropertyMetadata(string.Empty,
                    static (d, e) =>
                    {
                        ProductsViewSource source = CheckSource(d);
                        if (source.ProductDescription != string.Empty)
                            source.filters.Add(source.ProductDescriptionSearch);
                        else
                            source.filters.Remove(source.ProductDescriptionSearch);
                        source.Filter -= source.InternalFilter;
                        source.Filter += source.InternalFilter;
                    }, TrimString));



        public string ManufacturerName
        {
            get => (string)GetValue(ManufacturerNameProperty);
            set => SetValue(ManufacturerNameProperty, value);
        }

        public static readonly DependencyProperty ManufacturerNameProperty =
            DependencyProperty.Register(nameof(ManufacturerName),
                typeof(string),
                typeof(ProductsViewSource),
                new PropertyMetadata(string.Empty,
                    static (d, e) =>
                    {
                        ProductsViewSource source = CheckSource(d);
                        if (source.ManufacturerName != string.Empty)
                            source.filters.Add(source.ManufacturerNameSearch);
                        else
                            source.filters.Remove(source.ManufacturerNameSearch);
                        source.Filter -= source.InternalFilter;
                        source.Filter += source.InternalFilter;
                    }, TrimString));



        public string ProductCost
        {
            get => (string)GetValue(ProductCostProperty);
            set => SetValue(ProductCostProperty, value);
        }

        public static readonly DependencyProperty ProductCostProperty =
            DependencyProperty.Register(nameof(ProductCost),
                typeof(string),
                typeof(ProductsViewSource),
                new PropertyMetadata(string.Empty,
                    static (d, e) =>
                    {
                        ProductsViewSource source = CheckSource(d);
                        if (source.ProductCost != string.Empty)
                            source.filters.Add(source.ProductCostSearch);
                        else
                            source.filters.Remove(source.ProductCostSearch);
                        source.Filter -= source.InternalFilter;
                        source.Filter += source.InternalFilter;
                    }, TrimString));



        public string ProductQuantityInStock
        {
            get => (string)GetValue(ProductQuantityInStockProperty);
            set => SetValue(ProductQuantityInStockProperty, value);
        }

        public static readonly DependencyProperty ProductQuantityInStockProperty =
            DependencyProperty.Register(nameof(ProductQuantityInStock),
                typeof(string),
                typeof(ProductsViewSource),
                new PropertyMetadata(string.Empty,
                    static (d, e) =>
                    {
                        ProductsViewSource source = CheckSource(d);
                        if (source.ProductQuantityInStock != string.Empty)
                            source.filters.Add(source.ProductQuantityInStockSearch);
                        else
                            source.filters.Remove(source.ProductQuantityInStockSearch);
                        source.Filter -= source.InternalFilter;
                        source.Filter += source.InternalFilter;
                    }, TrimString));

        #region Методы-фильтры
        private bool ManufacturerFilter(IProduct product)
        {
            return product.ManufacturerId == ManufacturerId;
        }

        private bool ProductNameSearch(IProduct product)
        {
            return product.Name.Contains(ProductName,
                StringComparison.OrdinalIgnoreCase);
        }

        private bool ProductDescriptionSearch(IProduct product)
        {
            return product.Description.Contains(ProductDescription,
                StringComparison.OrdinalIgnoreCase);
        }

        private bool ManufacturerNameSearch(IProduct product)
        {
            return product.Manufacturer.Name.Contains(ManufacturerName,
                StringComparison.OrdinalIgnoreCase);
        }

        private bool ProductCostSearch(IProduct product)
        {
            return product.Cost.ToString().Contains(ProductCost,
                StringComparison.OrdinalIgnoreCase);
        }

        private bool ProductQuantityInStockSearch(IProduct product)
        {
            return product.QuantityInStock.ToString().Contains(ProductQuantityInStock,
                StringComparison.OrdinalIgnoreCase);
        }
        #endregion

        private void InternalFilter(object o, FilterEventArgs e)
        {
            var p = (IProduct)e.Item;
            foreach (var filter in filters)
            {
                if (!filter(p))
                {
                    e.Accepted = false;
                    return;
                }
            }
        }

        private static object TrimString(DependencyObject d, object baseValue)
        {
            return ((string?)baseValue)?.Trim() ?? string.Empty;
        }

        private readonly HashSet<Predicate<IProduct>> filters = new();
    }
}
