using InventoryManagementAPI.Data;
using InventoryManagementAPI.DTOs;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryItem>> GetInventoryItemsAsync();
        Task<InventoryItem> GetInventoryItemByIdAsync(int id);
        Task<InventoryItem> UpdateInventoryItemAsync(InventoryItem item);
        Task<bool> DeleteInventoryItemAsync(int id);
        Task<IEnumerable<InventoryItem>> GetInventoryItemsByCategoryAsync(string category);
        Task<IEnumerable<InventoryItem>> SearchInventoryItemsByNameAsync(string name);
    }

    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InventoryService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetTenantId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst("TenantId")?.Value;
        }


        public async Task<IEnumerable<InventoryItem>> GetInventoryItemsAsync()
        {

            try
            {
                var tenantId = GetTenantId();

                var items = await _context.InventoryItems
                    .FromSqlRaw(@"SELECT Id, Name, CAST(Price AS decimal(18,2)) AS Price, Quantity, Description, Category, TenantId 
                              FROM InventoryItems 
                              WHERE TenantId = {0}", tenantId)
                    .ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                throw new Exception("Error fetching inventory items", ex);
            }
        }





        public async Task<InventoryItem> GetInventoryItemByIdAsync(int id)
        {
            var tenantId = GetTenantId();
            return await _context.InventoryItems.FirstOrDefaultAsync(i => i.Id == id && i.TenantId == tenantId);
        }



        public async Task<InventoryItem> UpdateInventoryItemAsync(InventoryItem item)
        {
            var tenantId = GetTenantId();
            var existingItem = await _context.InventoryItems.FirstOrDefaultAsync(i => i.Id == item.Id && i.TenantId == tenantId);

            if (existingItem == null)
                return null;

            existingItem.Name = item.Name;
            existingItem.Description = item.Description;
            existingItem.Price = item.Price;
            existingItem.Quantity = item.Quantity;
            existingItem.Category = item.Category;

            _context.InventoryItems.Update(existingItem);
            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task<bool> DeleteInventoryItemAsync(int id)
        {
            var tenantId = GetTenantId();
            var item = await _context.InventoryItems.FirstOrDefaultAsync(i => i.Id == id && i.TenantId == tenantId);

            if (item == null)
                return false;

            _context.InventoryItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<InventoryItem>> GetInventoryItemsByCategoryAsync(string category)
        {
            var tenantId = GetTenantId();
            return await _context.InventoryItems.Where(i => i.TenantId == tenantId && i.Category == category).ToListAsync();
        }

        public async Task<IEnumerable<InventoryItem>> SearchInventoryItemsByNameAsync(string name)
        {
            var tenantId = GetTenantId();
            return await _context.InventoryItems.Where(i => i.TenantId == tenantId && i.Name.Contains(name)).ToListAsync();
        }


    }
}
