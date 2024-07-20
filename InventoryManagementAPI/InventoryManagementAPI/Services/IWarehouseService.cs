using InventoryManagementAPI.Data;
using InventoryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI.Services
{
    public interface IWarehouseService
    {
        Task<IEnumerable<Warehouse>> GetWarehousesAsync();
        Task<Warehouse> GetWarehouseByIdAsync(int id);
        Task<Warehouse> CreateWarehouseAsync(Warehouse warehouse);
        Task<Warehouse> UpdateWarehouseAsync(Warehouse warehouse);
        Task<bool> DeleteWarehouseAsync(int id);
        Task AssociateInventoryItemWithWarehouseAsync(int warehouseId, int inventoryItemId);
    }

    public class WarehouseService : IWarehouseService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WarehouseService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetTenantId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst("TenantId")?.Value;
        }

        public async Task<IEnumerable<Warehouse>> GetWarehousesAsync()
        {
            var tenantId = GetTenantId();
            return await _context.Warehouses.Where(w => w.TenantId == tenantId).ToListAsync();
        }

        public async Task<Warehouse> GetWarehouseByIdAsync(int id)
        {
            var tenantId = GetTenantId();
            return await _context.Warehouses.FirstOrDefaultAsync(w => w.Id == id && w.TenantId == tenantId);
        }

        public async Task<Warehouse> CreateWarehouseAsync(Warehouse warehouse)
        {
            warehouse.TenantId = GetTenantId();
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task<Warehouse> UpdateWarehouseAsync(Warehouse warehouse)
        {
            var tenantId = GetTenantId();
            var existingWarehouse = await _context.Warehouses.FirstOrDefaultAsync(w => w.Id == warehouse.Id && w.TenantId == tenantId);

            if (existingWarehouse == null)
                return null;

            existingWarehouse.Name = warehouse.Name;
            existingWarehouse.Location = warehouse.Location;

            _context.Warehouses.Update(existingWarehouse);
            await _context.SaveChangesAsync();
            return existingWarehouse;
        }

        public async Task<bool> DeleteWarehouseAsync(int id)
        {
            var tenantId = GetTenantId();
            var warehouse = await _context.Warehouses.FirstOrDefaultAsync(w => w.Id == id && w.TenantId == tenantId);

            if (warehouse == null)
                return false;

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AssociateInventoryItemWithWarehouseAsync(int warehouseId, int inventoryItemId)
        {
            var tenantId = GetTenantId();

            var warehouse = await _context.Warehouses
                .FirstOrDefaultAsync(w => w.Id == warehouseId && w.TenantId == tenantId);
            var inventoryItem = await _context.InventoryItems
                .FirstOrDefaultAsync(i => i.Id == inventoryItemId && i.TenantId == tenantId);

            if (warehouse == null || inventoryItem == null)
            {
                throw new InvalidOperationException("Warehouse or Inventory Item not found for the specified tenant.");
            }

            var warehouseInventoryItem = new WarehouseInventoryItem
            {
                WarehouseId = warehouseId,
                InventoryItemId = inventoryItemId
            };

            _context.Set<WarehouseInventoryItem>().Add(warehouseInventoryItem);

            await _context.SaveChangesAsync();
        }

    }
}
