using Microsoft.EntityFrameworkCore;
using ShopModel.Entities;

namespace ShopModel
{
    public partial class Context : DbContext
    {
        private const string CONNECTION = "server=localhost;password=curuserpass;user=root;database=newtrade";
        private static readonly ServerVersion _version = ServerVersion.Parse("8.0.30-mysql");

        public static Context Get()
        {
            var options = new DbContextOptionsBuilder<Context>().UseMySql(CONNECTION, _version).Options;
            return new(options);
        }

        public static Task<Context> GetAsync()
        {
            return Task.Run(Get);
        }

        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

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
                optionsBuilder.UseMySql(CONNECTION, _version);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PRIMARY");

                entity.ToTable("category");

                entity.Property(e => e.CategoryName).HasColumnType("text");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.HasKey(e => e.IdManufacturer)
                    .HasName("PRIMARY");

                entity.ToTable("manufacturer");

                entity.Property(e => e.ManufacturerName).HasColumnType("text");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.OrderStatus, "k1_idx");

                entity.HasIndex(e => e.OrderPickupPoint, "k2_idx");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ClientName).HasColumnType("text");

                entity.HasOne(d => d.OrderPickupPointNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderPickupPoint)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("key2");

                entity.HasOne(d => d.OrderStatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("key1");
            });

            modelBuilder.Entity<Orderproduct>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductArticleNumber })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("orderproduct");

                entity.HasIndex(e => e.ProductArticleNumber, "ProductArticleNumber");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductArticleNumber)
                    .HasMaxLength(100)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Orderproducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderproduct_ibfk_1");

                entity.HasOne(d => d.ProductArticleNumberNavigation)
                    .WithMany(p => p.Orderproducts)
                    .HasForeignKey(d => d.ProductArticleNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("orderproduct_ibfk_2");
            });

            modelBuilder.Entity<Orderstatus>(entity =>
            {
                entity.HasKey(e => e.IdOrderStatus)
                    .HasName("PRIMARY");

                entity.ToTable("orderstatus");

                entity.Property(e => e.StatusName).HasColumnType("text");
            });

            modelBuilder.Entity<Pickuppoint>(entity =>
            {
                entity.HasKey(e => e.IdPickupPoint)
                    .HasName("PRIMARY");

                entity.ToTable("pickuppoint");

                entity.Property(e => e.PickupPointAddress).HasColumnType("text");

                entity.Property(e => e.PickupPointIndex).HasMaxLength(6);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductArticleNumber)
                    .HasName("PRIMARY");

                entity.ToTable("product");

                entity.HasIndex(e => e.ProductUnit, "k0_idx");

                entity.HasIndex(e => e.ProductManufacturer, "k1_idx");

                entity.HasIndex(e => e.ProductSupplier, "k2_idx");

                entity.HasIndex(e => e.ProductCategory, "k3_idx");

                entity.Property(e => e.ProductArticleNumber)
                    .HasMaxLength(100)
                    .UseCollation("utf8mb3_general_ci")
                    .HasCharSet("utf8mb3");

                entity.Property(e => e.ProductCost).HasPrecision(19, 4);

                entity.Property(e => e.ProductDescription).HasColumnType("text");

                entity.Property(e => e.ProductName).HasColumnType("text");

                entity.HasOne(d => d.ProductCategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("k3");

                entity.HasOne(d => d.ProductManufacturerNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductManufacturer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("k1");

                entity.HasOne(d => d.ProductSupplierNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductSupplier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("k2");

                entity.HasOne(d => d.ProductUnitNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductUnit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("k0");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(100);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.IdSupplier)
                    .HasName("PRIMARY");

                entity.ToTable("supplier");

                entity.Property(e => e.SupplierName).HasColumnType("text");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasKey(e => e.IdUnit)
                    .HasName("PRIMARY");

                entity.ToTable("unit");

                entity.Property(e => e.UnitName).HasColumnType("text");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.UserRole, "UserRole");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserLogin).HasColumnType("text");

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.Property(e => e.UserPassword).HasColumnType("text");

                entity.Property(e => e.UserPatronymic).HasMaxLength(100);

                entity.Property(e => e.UserSurname).HasMaxLength(100);

                entity.HasOne(d => d.UserRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("user_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
