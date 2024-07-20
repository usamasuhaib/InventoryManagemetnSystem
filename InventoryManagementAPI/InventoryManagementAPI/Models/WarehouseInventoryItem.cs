namespace InventoryManagementAPI.Models
{
    public class WarehouseInventoryItem
    {
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }

        public int InventoryItemId { get; set; }
        public InventoryItem InventoryItem { get; set; }
    }
}
