using Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

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
        [NotMapped]
        public Rights Rights => Name switch
        {
            "Администратор" => Rights.Full,
            "Менеджер" => Rights.Updating,
            "Клиент" => Rights.Adding,
            _ => Rights.Viewing
        };

        public virtual ICollection<User> Users { get; } = null!;
    }
}
