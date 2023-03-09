using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ShopSQLite
{
    internal partial class CatalogContext : DbContext
    {
        private const string connectionString = "sqliteTest.db";

        public static CatalogContext Get()
        {
            return new ();
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderProduct> OrderProducts { get; set; } = null!;
        public virtual DbSet<Orderstatus> OrderStatuses { get; set; } = null!;
        public virtual DbSet<PickupPoint> PickuPpoints { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<Unit> Units { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {

                _ = optionsBuilder.UseSqlite(connectionString);

                optionsBuilder
                    .EnableSensitiveDataLogging()
                    .UseSqlite($"Data Source={connectionString}");
                optionsBuilder.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted });

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(Data.GetRoles());

            modelBuilder.Entity<Category>().HasData(Data.categories);

            modelBuilder.Entity<Manufacturer>().HasData(Data.manufacturers);

            modelBuilder.Entity<Supplier>().HasData(Data.suppliers);

            modelBuilder.Entity<Unit>().HasData(Data.unit);

            modelBuilder.Entity<User>().HasData(Data.GetUsers());

            modelBuilder.Entity<Product>().HasData(Data.GetProducts());

            modelBuilder.Entity<Orderstatus>().HasData(Data.orderstatuses);

            modelBuilder.Entity<PickupPoint>().HasData(Data.GetPickuppoints());

            modelBuilder.Entity<Order>().HasData(Data.GetOrders());

            modelBuilder.Entity<OrderProduct>().HasData(Data.GetOrderproduct());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
