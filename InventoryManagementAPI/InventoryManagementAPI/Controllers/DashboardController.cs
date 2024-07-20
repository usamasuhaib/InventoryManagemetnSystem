using InventoryManagementAPI.Data;
using InventoryManagementAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("counts")]
        public async Task<ActionResult<DashboardCountsDto>> GetDashboardCounts()
        {
            var tenantId = Request.Headers["TenantId"].ToString();
            var warehouseCount = await _context.Warehouses
                                               .Where(w => w.TenantId == tenantId)
                                               .CountAsync();

            var inventoryCount = await _context.InventoryItems
                                               .Where(i => i.TenantId == tenantId)
                                               .CountAsync();

            var counts = new DashboardCountsDto
            {
                WarehouseCount = warehouseCount,
                InventoryCount = inventoryCount
            };

            return Ok(counts);
        }

}
}
