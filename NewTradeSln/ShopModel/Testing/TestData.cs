namespace ShopModel.Testing
{
    public static partial class TestData
    {
        public static void RegisterTestFolder(string imageFolder)
        {
            TestData.imageFolder = imageFolder;
        }

        #region Константы и статические значения.
        private const string testFolder = @"Testing";
        private static readonly string appFolder = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string folderFullName = Path.Combine(appFolder, testFolder);

        private const string testProductsData = @"products.txt";
        private static readonly string productsDataFullName = Path.Combine(folderFullName, testProductsData);

        private const string testUsersData = @"users.txt";
        private static readonly string usersDataFullName = Path.Combine(folderFullName, testUsersData);

        private const string testRolesData = @"roles.txt";
        private static readonly string rolesDataFullName = Path.Combine(folderFullName, testRolesData);

        private static string imageFolder = appFolder;
        #endregion

    }
}
