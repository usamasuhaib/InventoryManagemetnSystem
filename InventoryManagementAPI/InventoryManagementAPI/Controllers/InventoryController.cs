using InventoryManagementAPI.Data;
using InventoryManagementAPI.DTOs;
using InventoryManagementAPI.Models;
using InventoryManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly IWarehouseService _warehouseService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public InventoryController(IInventoryService inventoryService, IWarehouseService warehouseService, IHttpContextAccessor httpContextAccessor, AppDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _inventoryService = inventoryService;
            _warehouseService = warehouseService;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _userManager = userManager;
        }


        [HttpGet("GetInventoryItems")]
        public async Task<IActionResult> GetInventoryItems()
        {
            try
            {
                var tenantId = Request.Headers["TenantId"].ToString();
                //var tenantId = "tenant2";

                if (string.IsNullOrEmpty(tenantId))
                {
                    return BadRequest("Tenant ID is missing");
                }

                var items = await _dbContext.InventoryItems
                    .FromSqlRaw(@"SELECT Id, Name, CAST(Price AS decimal(18,2)) AS Price, Quantity, Description, Category, TenantId 
                  FROM InventoryItems 
                  WHERE TenantId = {0}", tenantId)
                    .ToListAsync();
                
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("GetInventoryItemById/{id}")]
        public async Task<ActionResult<InventoryItem>> GetInventoryItemById(int id)
        {
            var tenantId = Request.Headers["TenantId"].ToString();

            if (string.IsNullOrEmpty(tenantId))
            {
                return BadRequest("Tenant ID is missing");
            }

            var item = await _dbContext.InventoryItems
                .FirstOrDefaultAsync(i => i.Id == id && i.TenantId == tenantId);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }


        [HttpPost("CreateInventoryItem")]
        public async Task<IActionResult> CreateInventoryItem([FromBody] InventoryItemDto inventoryItemDto)
        {
            if (ModelState.IsValid)
            {
                var tenantId = Request.Headers["TenantId"].ToString();

                if (string.IsNullOrEmpty(tenantId))
                {
                    return BadRequest("Tenant Id is missing");
                }

                var newItem = new InventoryItem
                {
                    Name = inventoryItemDto.Name,
                    Category = inventoryItemDto.Category,
                    Price = inventoryItemDto.Price,
                    Description = inventoryItemDto.Description,
                    Quantity = inventoryItemDto.Quantity,
                    TenantId = tenantId
                };

                try
                {
                    // Add new item to the context
                    await _dbContext.InventoryItems.AddAsync(newItem);
                    await _dbContext.SaveChangesAsync();

                    var warehouse = await _dbContext.Warehouses
                        .FirstOrDefaultAsync(w => w.Id == inventoryItemDto.wareHouseId && w.TenantId == tenantId);

                    if (warehouse == null)
                    {
                        return BadRequest($"Invalid Warehouse Id: {inventoryItemDto.wareHouseId}");
                    }

                    var warehouseInventoryItem = new WarehouseInventoryItem
                    {
                        WarehouseId = inventoryItemDto.wareHouseId,
                        InventoryItemId = newItem.Id
                    };

                    _dbContext.Set<WarehouseInventoryItem>().Add(warehouseInventoryItem);
                    await _dbContext.SaveChangesAsync();

                    return Ok(new
                    {
                        Result = "New Item Added Successfully"
                    });
                }
                catch (Exception ex)
                {
                    // Log exception
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

            return BadRequest(new { Result = "Failed to add new item" });
        }


        private string GetCurrentTenantId()
        {
            return _httpContextAccessor.HttpContext?.Items["TenantId"] as string ?? "default_tenant";
        }


        [Authorize(Policy = "AdminOnly")]

        [HttpPut("UpdateInventory/{id}")]
        public async Task<IActionResult> UpdateInventoryItem(int id, [FromBody] InventoryItemDto inventoryItemDto)
        {
            var tenantId = Request.Headers["TenantId"].ToString();

            if (string.IsNullOrEmpty(tenantId))
            {
                return BadRequest("Tenant ID is missing");
            }
            

            var existingItem = await _dbContext.InventoryItems
                .FirstOrDefaultAsync(i => i.Id == id && i.TenantId == tenantId);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Name = inventoryItemDto.Name;
            existingItem.Description = inventoryItemDto.Description;
            existingItem.Price = inventoryItemDto.Price;
            existingItem.Quantity = inventoryItemDto.Quantity;
            existingItem.Category = inventoryItemDto.Category;

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            return Ok(existingItem);
        }



        [HttpDelete("DeleteInventoryItem/{id}")]
        public async Task<IActionResult> DeleteInventoryItem(int id)
        {
            var tenantId = Request.Headers["TenantId"].ToString();

            if (string.IsNullOrEmpty(tenantId))
            {
                return BadRequest("Tenant ID is missing");
            }

            var item = await _dbContext.InventoryItems
                .FirstOrDefaultAsync(i => i.Id == id && i.TenantId == tenantId);

            if (item == null)
            {
                return NotFound();
            }

            _dbContext.InventoryItems.Remove(item);

      
            await _dbContext.SaveChangesAsync();

            return NoContent(); 
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<InventoryItem>>> GetInventoryItemsByCategory(string category)
        {
            var items = await _inventoryService.GetInventoryItemsByCategoryAsync(category);
            return Ok(items);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<InventoryItem>>> SearchInventoryItemsByName(string name)
        {
            var items = await _inventoryService.SearchInventoryItemsByNameAsync(name);
            return Ok(items);
        }
    }
}
