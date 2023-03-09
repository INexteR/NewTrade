namespace ShopSQLite.Entities
{
    [Table("roles")]
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public int Id { get; internal set; }
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; } = null!;
    }
}
