using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ShopSQLite.Entities;
using ShopSQLite.Initialization;

namespace ShopSQLite
{
    internal partial class CatalogContext : DbContext
    {

        private readonly string сonnectionString;

        public static CatalogContext Get(string сonnectionString)
        {
            return new CatalogContext(сonnectionString);
        }

        public CatalogContext(string сonnectionString)
            => this.сonnectionString = сonnectionString;

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Manufacturer> Manufacturers { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderProduct> Orderproducts { get; set; } = null!;
        public virtual DbSet<Orderstatus> Orderstatuses { get; set; } = null!;
        public virtual DbSet<PickupPoint> Pickuppoints { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;
        public virtual DbSet<Unit> Units { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {

                _ = optionsBuilder.UseSqlite(сonnectionString);

                optionsBuilder
                    .EnableSensitiveDataLogging()
                    .UseSqlite($"Data Source={сonnectionString}");
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
