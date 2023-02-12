
namespace Interfaces
{
    public class AuthorizationChangedArgs : EventArgs
    {
        public AuthorizationStatus NewStatus { get; }
        public IUser? NewUser { get; }

        public AuthorizationChangedArgs(AuthorizationStatus newStatus, IUser? newUser)
        {
            NewStatus = newStatus;
            NewUser = newUser;
        }
    }
}
