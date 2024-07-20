using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.DTOs
{
    public class UpdateInventoryItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public string TenantId { get; set; } // Add TenantId property

    }
}
