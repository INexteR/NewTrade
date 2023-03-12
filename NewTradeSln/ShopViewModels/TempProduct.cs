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
            DiscountAmount = -1;
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

        public int UnitId
        { 
            get => Get<int>(); 
            set
            {
                ClearErrors();
                if (value is -1)
                {
                    AddError(unitError);
                }
                Set(value);
            }
        }

        public int ManufacturerId
        { 
            get => Get<int>();
            set
            {
                ClearErrors();
                if (value is -1)
                {
                    AddError(manufacturerError);
                }
                Set(value);
            }
        }

        public int SupplierId
        { 
            get => Get<int>(); 
            set 
            {
                ClearErrors();
                if (value is -1)
                {
                    AddError(supplierError);
                }
                Set(value);
            } 
        }
        public int CategoryId
        { 
            get => Get<int>();
            set
            {
                ClearErrors();
                if (value is -1)
                {
                    AddError(categoryError);
                }
                Set(value);
            }
        }

        public int Id { get; private set; }
        public IUnit Unit { get; }
        public IManufacturer Manufacturer { get; }
        public ISupplier Supplier { get; }
        public ICategory Category { get; }
        public IEnumerable<IOrderProduct> OrderProducts { get; }

        private const string nameError = "Введите имя";
        private const string unitError = "Выберите единицу измерения";
        private const string costError = "Введите цену";
        private const string manufacturerError = "Выберите производителя";
        private const string supplierError = "Выберите поставщика";
        private const string categoryError = "Выберите категорию";
        private const string maxDiscountAmountError = "Введите максимальную скидку";
        private const string quantityInStockError = "Введите количество на складе";
        private const string descriptionError = "Введите описание";

    }
}
