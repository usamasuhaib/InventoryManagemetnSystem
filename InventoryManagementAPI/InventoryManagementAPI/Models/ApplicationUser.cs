using Microsoft.AspNetCore.Identity;

namespace InventoryManagementAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }

}
