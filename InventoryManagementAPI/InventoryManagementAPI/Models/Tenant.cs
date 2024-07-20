namespace InventoryManagementAPI.Models
{
    public class Tenant
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }

}
