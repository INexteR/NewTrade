using Model;
using ViewModels;

namespace ShopViewModels
{
    public class TempProduct : ValidationBase, IProduct
    {
        public TempProduct()
        {
            Name = string.Empty;
            Cost = -1;
            MaxDiscountAmount = -1;
            QuantityInStock = -1;
            Description = string.Empty;
            Unit = null;
            Manufacturer = null;
            Supplier = null;
            Category = null;
        }

        public string Name
        {
            get => Get<string>()!;
            set
            {
                ClearErrors();
                if (string.IsNullOrWhiteSpace(value))
                {
                    AddError(nameError);
                }
                Set(value);
            }
        }

        public decimal Cost
        {
            get => Get<decimal>();
            set
            {
                ClearErrors();
                if (value is -1)
                {
                    AddError(costError);
                }
                Set(value);
            }
        }

        public int MaxDiscountAmount 
        { 
            get => Get<int>();
            set
            {
                ClearErrors();
                if (value is -1)
                {
                    AddError(maxDiscountAmountError);
                }
                Set(value);
            }
        }

        public sbyte? DiscountAmount { get => Get<sbyte?>(); set => Set(value); }

        public int QuantityInStock
        {
            get => Get<int>();
            set
            {
                ClearErrors();
                if (value is -1)
                {
                    AddError(quantityInStockError);
                }
                Set(value);
            }
        }

        public string Description 
        { 
            get => Get<string>()!;
            set
            {
                ClearErrors();
                if (string.IsNullOrWhiteSpace(value))
                {
                    AddError(descriptionError);
                }
                Set(value);
            }
        }

        public string? Path { get => Get<string>()!; set => Set(value); }

        public IUnit? Unit 
        { 
            get => Get<IUnit?>(); 
            set
            {
                ClearErrors();
                if (value is null)
                {
                    AddError(unitError);
                }
                Set(value);
            }
        }

        public IManufacturer? Manufacturer 
        { 
            get => Get<IManufacturer?>();
            set
            {
                ClearErrors();
                if (value is null)
                {
                    AddError(manufacturerError);
                }
                Set(value);
            }
        }

        public ISupplier? Supplier 
        { 
            get => Get<ISupplier?>(); 
            set 
            {
                ClearErrors();
                if (value is null)
                {
                    AddError(supplierError);
                }
                Set(value);
            } 
        }
        public ICategory? Category 
        { 
            get => Get<ICategory?>();
            set
            {
                ClearErrors();
                if (value is null)
                {
                    AddError(categoryError);
                }
                Set(value);
            }
        }

        private const string nameError = "Введите имя";
        private const string unitError = "Выберите единицу измерения";
        private const string costError = "Введите цену";
        private const string manufacturerError = "Выберите производителя";
        private const string supplierError = "Выберите поставщика";
        private const string categoryError = "Выберите категорию";
        private const string maxDiscountAmountError = "Введите максимальную скидку";
        private const string quantityInStockError = "Введите количество на складе";
        private const string descriptionError = "Введите описание";

        int IProduct.Id => throw new NotImplementedException();
        int IProduct.UnitId => Unit?.Id ?? -1;
        int IProduct.ManufacturerId => Manufacturer?.Id ?? -1;
        int IProduct.SupplierId => Supplier?.Id ?? -1;
        int IProduct.CategoryId => Category?.Id ?? -1;
        IEnumerable<IOrderProduct> IProduct.OrderProducts => throw new NotImplementedException();
    }
}
