namespace ShopSQLite.Initialization
{
    internal static partial class Data
    {
        private static Role[]? roles;
        public static Role[] GetRoles()
        {
            return roles ??= rolesText.ParseToArray<Role>(nameof(Role.Id), nameof(Role.Name));
        }

        private const string rolesText= @"
1	Администратор
2	Менеджер
3	Клиент
4	Гость
";
    }
}
