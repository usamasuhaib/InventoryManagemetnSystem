namespace InventoryManagementAPI.Models
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public string TenantId { get; set; } // Discriminator column for multi-tenancy
    }

}
