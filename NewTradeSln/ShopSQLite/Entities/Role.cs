using Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopSQLite.Entities
{
    [Table("roles")]
    internal partial class Role : IRole
    {
        [Key]
        public int Id { get; internal set; }

        private string _name = string.Empty;
        public string Name { get => _name; internal set => _name = value ?? string.Empty; }
        public Rights Rights { get; internal set; }

        private ICollection<User>? users;

        public virtual ICollection<User> Users
        {
            get => users ??= new HashSet<User>();
            set => users = value;
        }
    }
}
