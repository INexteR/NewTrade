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
        [NotMapped]        
        public Rights Rights => rights ??= (Name switch
        {
            "Администратор" => Rights.Full,
            "Менеджер" => Rights.AddingAndUpdating,
            _ => Rights.Viewing
        });
        private Rights? rights;

        public virtual ICollection<User> Users { get; } = null!;
    }
}
