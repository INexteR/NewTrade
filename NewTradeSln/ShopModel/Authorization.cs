using Interfaces;
using ShopModel.Entities;
using ShopModel.DTOs;
using Microsoft.EntityFrameworkCore;

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
                new Role { RoleId = 1, RoleName = "Администратор" },
                new Role { RoleId = 2, RoleName = "Менеджер" },
                new Role { RoleId = 3, RoleName = "Клиент" },
            };

            var lines = File.ReadAllLines("../../../../ShopModel/users.txt");

            foreach (var line in lines)
            {
                string[] props = line.Split('\t');
                if (props[4] == login && props[5] == password)
                {
                    CurrentUser = new(new User
                    {
                        UserId = int.Parse(props[0]),
                        UserSurname = props[1],
                        UserName = props[2],
                        UserPatronymic = props[3],
                        UserLogin = props[4],
                        UserPassword = props[5],
                        UserRoleNavigation = roles[int.Parse(props[6]) - 1]
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
