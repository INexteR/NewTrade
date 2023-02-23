using Common;
using Interfaces;
using ShopModel.Entities;

namespace ShopModel.Testing
{
    internal static partial class TestData
    {
        private static readonly Manufacturer[] manufacturers =
            {
                new Manufacturer{ Id = 1, Name = "БТК Текстиль" },
                new Manufacturer{ Id = 2, Name = "Империя ткани" },
                new Manufacturer{ Id = 3, Name = "Комильфо" },
                new Manufacturer{ Id = 4, Name = "Май Фабрик" }
            };
        public static IEnumerable<Manufacturer> GetManufacturers()
            => manufacturers.Select(m => m);

    }
}
