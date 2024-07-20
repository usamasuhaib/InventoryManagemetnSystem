using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.DTOs
{
    public class UpdateWarehouseDto 
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string TenantId { get; set; } // Add TenantId property

    }
}
