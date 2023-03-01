using Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopSQLite.Entities
{
    [Table("roles")]
    internal partial class Role : IRole
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public int Id { get; internal set; }
        [MaxLength(100)]
        public string Name { get; set; } = null!;
        public Rights Rights { get; internal set; }

        public virtual ICollection<User> Users { get; } = null!;
    }
}
