namespace InventoryManagementAPI.Models
{
    public class InventoryItem : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public ICollection<WarehouseInventoryItem> WarehouseInventoryItems { get; set; }
    }

}
