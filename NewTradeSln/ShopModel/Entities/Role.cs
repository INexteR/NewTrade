using Interfaces;

namespace ShopModel.Entities
{
    public partial class Role : IRole
    {
        public int Id { get; internal set; }

        private string _name = string.Empty;
        public string Name { get => _name; internal set => _name = value ?? string.Empty; }
        public Rights Rights { get; internal set; }

        private ICollection<User>? users;

        public virtual ICollection<User> _Users
        {
            get => users ??= new HashSet<User>();
            set => users = value;
        }
    }
}
