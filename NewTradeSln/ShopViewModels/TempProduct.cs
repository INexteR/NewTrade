using Model;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShopViewModels
{
    public class TempProduct : ValidationBase, IProduct
    {
        public TempProduct()
        {
            Name = string.Empty;
            Cost = -1;
            DiscountAmount = -1;
            MaxDiscountAmount = -1;
            QuantityInStock = -1;
            Description = string.Empty;
            UnitId = -1;
            ManufacturerId = -1;
            SupplierId = -1;
            CategoryId = -1;
        }

        public string Name
        {
            get => Get<string>()!;
            set => Set(value);
        }

        public decimal Cost
        {
            get => Get<decimal>();
            set => Set(value);
        }

        public int MaxDiscountAmount
        {
            get => Get<int>();
            set => Set(value);
        }

        public sbyte? DiscountAmount
        {
            get => Get<sbyte?>();
            set => Set(value);
        }

        public int QuantityInStock
        {
            get => Get<int>();
            set => Set(value);
        }

        public string Description
        {
            get => Get<string>()!;
            set => Set(value);
        }

        public string? Path { get => Get<string>()!; set => Set(value); }

        public int UnitId
        {
            get => Get<int>();
            set => Set(value);
        }

        public int ManufacturerId
        {
            get => Get<int>();
            set => Set(value);
        }

        public int SupplierId
        {
            get => Get<int>();
            set => Set(value);
        }
        public int CategoryId
        {
            get => Get<int>();
            set => Set(value);
        }

        protected override void OnPropertyChanged(string propertyName, object? oldValue, object? newValue)
        {
            base.OnPropertyChanged(propertyName, oldValue, newValue);
            if (propertyName is nameof(QuantityInStock) or nameof(UnitId) or nameof(ManufacturerId) or nameof(SupplierId) or nameof(CategoryId)
                                or nameof(Cost)
                                or nameof(Name) or nameof(Description)
                                or nameof(MaxDiscountAmount) or nameof(DiscountAmount))
            {
                ClearErrors(propertyName);
                bool isError = propertyName is nameof(DiscountAmount)
                               ? false
                               : propertyName is nameof(Cost)
                               ? (decimal)newValue! <= 0
                               : propertyName is nameof(Name) or nameof(Description)
                               ? string.IsNullOrWhiteSpace((string?)newValue)
                               : (int)newValue! <= 0;
                if (isError)
                {
                    AddError(errors[propertyName], propertyName);
                }
                else if (propertyName is nameof(MaxDiscountAmount))
                {
                    if (!DiscountAmount.HasValue || (int)newValue! < DiscountAmount.Value)
                    {
                        AddError(maxDiscountAmountRangeError);
                    }
                    else
                    {
                        ClearErrors(nameof(DiscountAmount));
                    }
                }
                else if (propertyName is nameof(DiscountAmount))
                {
                    if ((sbyte?)newValue > MaxDiscountAmount)
                    {
                        AddError(discountAmountRangeError);
                    }
                    else if (MaxDiscountAmount != -1)
                        ClearErrors(nameof(MaxDiscountAmount));
                }

                Set(newValue, propertyName);
            }

        }

        public int Id { get; private set; }
        IUnit IProduct.Unit => throw new NotImplementedException();
        IManufacturer IProduct.Manufacturer => throw new NotImplementedException();
        ISupplier IProduct.Supplier => throw new NotImplementedException();
        ICategory IProduct.Category => throw new NotImplementedException();
        IEnumerable<IOrderProduct> IProduct.OrderProducts => throw new NotImplementedException();

        private const string nameError = "Введите название";
        private const string unitError = "Выберите единицу измерения";
        private const string costError = "Введите цену";
        private const string manufacturerError = "Выберите производителя";
        private const string supplierError = "Выберите поставщика";
        private const string categoryError = "Выберите категорию";
        private const string maxDiscountAmountError = "Введите максимальную скидку";
        private const string quantityInStockError = "Введите количество на складе";
        private const string descriptionError = "Введите описание";

        private const string maxDiscountAmountRangeError = "Максимальная скидка не может быть меньше скидки";
        private const string discountAmountRangeError = "Скидка не может быть больше максимальной скидки";


        private static ReadOnlyDictionary<string, string> errors = new(new Dictionary<string, string>()
        {
            {nameof(Name), nameError},
            {nameof(UnitId), unitError},
            {nameof(Cost), costError },
            {nameof(ManufacturerId), manufacturerError },
            {nameof(SupplierId), supplierError },
            {nameof(CategoryId), categoryError },
            {nameof(MaxDiscountAmount), maxDiscountAmountError },
            {nameof(QuantityInStock), quantityInStockError },
            {nameof(Description), descriptionError }
        });
    }
}
