using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace InventoryManagementAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<WarehouseInventoryItem> WarehouseInventoryItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Seed tenants
            builder.Entity<Tenant>().HasData(
                new Tenant { Id = "tenant1", Name = "ABC Company" },
                new Tenant { Id = "tenant2", Name = "XYZ Company" }
            );

            // Seed Warehouses
            builder.Entity<Warehouse>().HasData(
                new Warehouse { Id = 1, Name = "Warehouse A", Location = "Location A", TenantId = "tenant1" },
                new Warehouse { Id = 2, Name = "Warehouse B", Location = "Location B", TenantId = "tenant2" },
                new Warehouse { Id = 3, Name = "Warehouse C", Location = "Location C", TenantId = "tenant2" },
                new Warehouse { Id = 4, Name = "Warehouse D", Location = "Location A",TenantId = "tenant1" }
                
            );

            // Seed InventoryItems
            builder.Entity<InventoryItem>().HasData(
                new InventoryItem { Id = 1, Name = "Item 1", Description = "Description 1", Price = 10, Quantity = 100, Category = "Category A", TenantId = "tenant1" },
                new InventoryItem { Id = 2, Name = "Item 2", Description = "Description 2", Price = 20, Quantity = 50, Category = "Category B", TenantId = "tenant1" },
                new InventoryItem { Id = 3, Name = "Item 3", Description = "Description 3", Price = 15, Quantity = 75, Category = "Category A", TenantId = "tenant1" },
                new InventoryItem { Id = 4, Name = "Item 4", Description = "Description 4", Price = 50, Quantity = 200, Category = "Category C", TenantId = "tenant2" },
                new InventoryItem { Id = 5, Name = "Item 5", Description = "Description 5", Price = 68, Quantity = 80, Category = "Category A", TenantId = "tenant1" },
                new InventoryItem { Id = 6, Name = "Item 6", Description = "Description 6", Price = 100, Quantity = 95, Category = "Category B", TenantId = "tenant2" },
                 new InventoryItem { Id = 7, Name = "Item8", Description = "Description1", Price = 100, Quantity = 10, Category = "Category1", TenantId = "tenant1" },
                new InventoryItem { Id = 8, Name = "Item9", Description = "Description2", Price = 200, Quantity = 20, Category = "Category2", TenantId = "tenant2" },
                new InventoryItem { Id = 9, Name = "Item10", Description = "Description3", Price = 300, Quantity = 30, Category = "Category3", TenantId = "tenant1" },
                new InventoryItem { Id = 10, Name = "Item11", Description = "Description4", Price = 400, Quantity = 40, Category = "Category4", TenantId = "tenant2" }

                );

            // WarehouseInventoryItems (Many-to-many relationships)
            builder.Entity<WarehouseInventoryItem>().HasData(
                new WarehouseInventoryItem { WarehouseId = 1, InventoryItemId = 1 },
                new WarehouseInventoryItem { WarehouseId = 1, InventoryItemId = 2 },
                new WarehouseInventoryItem { WarehouseId = 2, InventoryItemId = 1 },
                new WarehouseInventoryItem { WarehouseId = 3, InventoryItemId = 3 },
                new WarehouseInventoryItem { WarehouseId = 4, InventoryItemId = 4 },
                new WarehouseInventoryItem { WarehouseId = 3, InventoryItemId = 6 },
                new WarehouseInventoryItem { WarehouseId = 1, InventoryItemId = 7 },
                new WarehouseInventoryItem { WarehouseId = 3, InventoryItemId = 8 },
                new WarehouseInventoryItem { WarehouseId = 4, InventoryItemId = 9 },
                new WarehouseInventoryItem { WarehouseId = 4, InventoryItemId = 10 }

            );

            builder.Entity<WarehouseInventoryItem>()
        .HasKey(wi => new { wi.WarehouseId, wi.InventoryItemId });
            
            builder.Entity<WarehouseInventoryItem>()
                .HasOne(wi => wi.Warehouse)
                .WithMany(w => w.WarehouseInventoryItems)
                .HasForeignKey(wi => wi.WarehouseId);

            builder.Entity<WarehouseInventoryItem>()
                .HasOne(wi => wi.InventoryItem)
                .WithMany(i => i.WarehouseInventoryItems)
                .HasForeignKey(wi => wi.InventoryItemId);



            // One-to-many relationship between Tenant and ApplicationUser
            builder.Entity<Tenant>()
                .HasMany(t => t.Users)
                .WithOne(u => u.Tenant)
                .HasForeignKey(u => u.TenantId)
                .IsRequired();
            builder.Entity<InventoryItem>()
                .Property(i => i.Price)
                .HasPrecision(18, 2);


        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new IdentityRole() { Name = "Manager", ConcurrencyStamp = "2", NormalizedName = "MANAGER" }
                );
        }

        private string GetTenantId()
        {
            return _httpContextAccessor.HttpContext?.Items["TenantId"] as string ?? "default_tenant";
        }

        public override int SaveChanges()
        {
            ApplyTenantId();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyTenantId();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyTenantId()
        {
            var tenantId = GetTenantId();
            foreach (var entry in ChangeTracker.Entries<EntityBase>().Where(e => e.State == EntityState.Added))
            {
                entry.Entity.TenantId = tenantId;
            }
        }
    }
}
