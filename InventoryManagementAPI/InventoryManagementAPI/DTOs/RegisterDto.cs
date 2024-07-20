namespace InventoryManagementAPI.DTOs
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string TenantId { get; set; } // Add TenantId property
    }
}
