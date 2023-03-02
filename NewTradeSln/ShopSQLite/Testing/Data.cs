using ShopSQLite.Entities;

namespace ShopSQLite.Initialization
{
    internal static partial class Data
    {
        public static readonly Manufacturer[] manufacturers = new[]
        {
            new Manufacturer{ Id = 1, Name = "БТК Текстиль" },
            new Manufacturer{ Id = 2, Name = "Империя ткани" },
            new Manufacturer{ Id = 3, Name = "Комильфо" },
            new Manufacturer{ Id = 4, Name = "Май Фабрик" }
        };

        public static readonly Supplier[] suppliers =
        {
            new Supplier { Id = 1, Name = "Раута" },
            new Supplier { Id = 2, Name = "ООО Афо-Тек" },
            new Supplier { Id = 3, Name = "ГК Петров" }
        };

        public static readonly Category[] categories =
        {
            new Category { Id = 1, Name = "Постельные ткани" },
            new Category { Id = 2, Name = "Детские ткани" },
            new Category { Id = 3, Name = "Ткани для изделий" }
        };

        public static Unit unit = new(){ Id = 1, Name = "шт." };

        public static readonly Orderstatus[] orderstatuses =
        {
            new Orderstatus { Id = 1, Name = "Новый" },
            new Orderstatus { Id = 2, Name = "Завершен"}
        };


    }
}
