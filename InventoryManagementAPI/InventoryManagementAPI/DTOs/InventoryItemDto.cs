using InventoryManagementAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementAPI.DTOs
{
    public class InventoryItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public int wareHouseId { get; set; }

    }
}
