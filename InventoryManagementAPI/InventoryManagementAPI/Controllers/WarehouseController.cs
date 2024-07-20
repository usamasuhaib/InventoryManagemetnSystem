using InventoryManagementAPI.Data;
using InventoryManagementAPI.DTOs;
using InventoryManagementAPI.Models;
using InventoryManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;
        private readonly AppDbContext _dbContext;

        public WarehouseController(IWarehouseService warehouseService,AppDbContext dbContext)
        {
            _warehouseService = warehouseService;
            _dbContext = dbContext;
        }

  
        [HttpGet("GetWarehouses")]
        public async Task<IActionResult> GetWarehouses()
        {
            try
            {
                var tenantId = Request.Headers["TenantId"].ToString();
                //var tenantId = "tenant2";

                if (string.IsNullOrEmpty(tenantId))
                {
                    return BadRequest("Tenant ID is missing");
                }

                var items = await _dbContext.Warehouses
                    .FromSqlRaw(@"SELECT Id, Name , Location,  TenantId 
                  FROM Warehouses 
                  WHERE TenantId = {0}", tenantId)
                    .ToListAsync();

                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> GetWarehouse(int id)
        {
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);
            if (warehouse == null)
                return NotFound();
            return Ok(warehouse);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Warehouse>> CreateWarehouse(Warehouse warehouse)
        {
            var createdWarehouse = await _warehouseService.CreateWarehouseAsync(warehouse);
            return CreatedAtAction(nameof(GetWarehouse), new { id = createdWarehouse.Id }, createdWarehouse);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateWarehouse(int id, Warehouse warehouse)
        {
            if (id != warehouse.Id)
                return BadRequest();

            var updatedWarehouse = await _warehouseService.UpdateWarehouseAsync(warehouse);
            if (updatedWarehouse == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            var success = await _warehouseService.DeleteWarehouseAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpPost("{warehouseId}/inventory/{inventoryItemId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> AssociateInventoryItemWithWarehouse(int warehouseId, int inventoryItemId)
        {
            await _warehouseService.AssociateInventoryItemWithWarehouseAsync(warehouseId, inventoryItemId);
            return NoContent();
        }
    }
}
