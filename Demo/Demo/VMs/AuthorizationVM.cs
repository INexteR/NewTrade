namespace Demo.VMs
{
    class AuthorizationVM : BaseInpc
    {
        public User? Authorize(string login, string password)
        {
            using var context = CatalogContext.Get();
            foreach (var user in context.Users)
            {
                if (user.Login == login && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
