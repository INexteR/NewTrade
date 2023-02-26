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
        public virtual DbSet<Orderproduct> Orderproducts { get; set; } = null!;
        public virtual DbSet<Orderstatus> Orderstatuses { get; set; } = null!;
        public virtual DbSet<Pickuppoint> Pickuppoints { get; set; } = null!;
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

            modelBuilder.Entity<Order>(entity =>
             {
                 entity.ToTable("orders");

                 entity.HasIndex(e => e.OrderStatusId, "k1_idx");

                 entity.HasIndex(e => e.PickupPointId, "k2_idx");

                 entity.Property(e => e.ClientName).HasColumnType("text");

                 entity.HasOne(d => d.OrderStatus)
                     .WithMany(p => p.Orders)
                     .HasForeignKey(d => d.OrderStatusId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("orderstatus");

                 entity.HasOne(d => d.PickupPoint)
                     .WithMany(p => p.Orders)
                     .HasForeignKey(d => d.PickupPointId)
                     .OnDelete(DeleteBehavior.ClientSetNull)
                     .HasConstraintName("pickuppoint");
             });

            modelBuilder.Entity<Orderproduct>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductArticleNumber })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("orderproduct");

                entity.HasIndex(e => e.ProductArticleNumber, "k2_idx");

                entity.Property(e => e.ProductArticleNumber).HasMaxLength(100);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderproducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("order");

                entity.HasOne(d => d.ProductArticleNumberNavigation)
                    .WithMany(p => p.Orderproducts)
                    .HasForeignKey(d => d.ProductArticleNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("product");
            });

            modelBuilder.Entity<Orderstatus>(entity =>
            {
                entity.ToTable("orderstatus");

                entity.Property(e => e.Name).HasColumnType("text");
            });

            modelBuilder.Entity<Pickuppoint>(entity =>
            {
                entity.ToTable("pickuppoint");

                entity.Property(e => e.Address).HasColumnType("text");

                entity.Property(e => e.Index).HasMaxLength(6);
            });






            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
