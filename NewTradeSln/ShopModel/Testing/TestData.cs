
using ShopModel.Entities;

namespace ShopModel.Testing
{
    internal static partial class TestData
    {
        #region Константы и статические значения.
        private const string testFolder = @"ShopModel/Testing";
        private static readonly string appFolder = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string folderFullName = Path.Combine(appFolder, "../../../../" , testFolder);

        private const string testProductsData = @"products.txt";
        private static readonly string productsDataFullName = Path.Combine(folderFullName, testProductsData);

        private const string testUsersData = @"users.txt";
        private static readonly string usersDataFullName = Path.Combine(folderFullName, testUsersData);

        private const string testRolesData = @"roles.txt";
        private static readonly string rolesDataFullName = Path.Combine(folderFullName, testRolesData);
        #endregion

        private static readonly Manufacturer[] manufacturers = new[]
        {
            new Manufacturer{ Id = 1, Name = "БТК Текстиль" },
            new Manufacturer{ Id = 2, Name = "Империя ткани" },
            new Manufacturer{ Id = 3, Name = "Комильфо" },
            new Manufacturer{ Id = 4, Name = "Май Фабрик" }
        };

        private static readonly Supplier[] suppliers =
        {
            new Supplier { Id = 1, Name = "Раута" },
            new Supplier { Id = 2, Name = "ООО Афо-Тек" },
            new Supplier { Id = 3, Name = "ГК Петров" }
        };

        private static readonly Category[] categories =
        {
            new Category { Id = 1, Name = "Постельные ткани" },
            new Category { Id = 2, Name = "Детские ткани" },
            new Category { Id = 3, Name = "Ткани для изделий" }
        };

        private static Unit unit = new(){ Id = 1, Name = "шт." };
    }
}
