using Interfaces;
using ShopModel.Entities;
using ShopModel.DTOs;

namespace ShopModel
{
    public class Authorization
    {
        public UserDTO? Login(string login, string password)
        {
            AuthorizationStatus = AuthorizationStatus.InProcessing;
            //using var context = Context.Get();
            //foreach (var user in context.Users.ToList())
            //{
            //    if (user.UserLogin == login && user.UserPassword == password)
            //    {
            //        context.Entry(user).Reference(u => u.UserRoleNavigation).Load();
            //        CurrentUser = new(user);
            //        AuthorizationStatus = AuthorizationStatus.Authorized;
            //        return CurrentUser;
            //    }
            //}

            Role[] roles =
{
                new Role { Id = 1, Name = "Администратор" },
                new Role { Id = 2, Name = "Менеджер" },
                new Role { Id = 3, Name = "Клиент" },
            };

            var lines = File.ReadAllLines("../../../../ShopModel/users.txt");

            foreach (var line in lines)
            {
                string[] props = line.Split('\t');
                if (props[4] == login && props[5] == password)
                {
                    CurrentUser = new(new User
                    {
                        Id = int.Parse(props[0]),
                        Surname = props[1],
                        Name = props[2],
                        Patronymic = props[3],
                        Login = props[4],
                        Password = props[5],
                        Role = roles[int.Parse(props[6]) - 1]
                    });
                    AuthorizationStatus = AuthorizationStatus.Authorized;
                    return CurrentUser;
                }
            }

            AuthorizationStatus = AuthorizationStatus.Fail;
            return null;
        }

        public UserDTO? CurrentUser { get; private set; }

        public event EventHandler<AuthorizationStatus>? StatusChanged;

        private AuthorizationStatus _authorizationStatus;
        public AuthorizationStatus AuthorizationStatus
        {
            get => _authorizationStatus;
            set
            {
                if (Shop.Set(ref _authorizationStatus, value))
                    StatusChanged?.Invoke(this, value);
            }
        }

        public void LoginAsGuest()
        {
            AuthorizationStatus = AuthorizationStatus.Authorized;
        }

        public void Exit()
        {
            CurrentUser = null;
            _authorizationStatus = AuthorizationStatus.None;
        }
    }
}
