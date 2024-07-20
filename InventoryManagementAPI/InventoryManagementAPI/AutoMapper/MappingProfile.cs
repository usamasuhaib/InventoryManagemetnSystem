using InventoryManagementAPI.DTOs;
using InventoryManagementAPI.Models;
using AutoMapper;


namespace InventoryManagementAPI.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InventoryItem, InventoryItemDto>();
            CreateMap<UpdateInventoryItemDto, InventoryItem>();



            CreateMap<Warehouse, WarehouseDto>();
            CreateMap<UpdateWarehouseDto, Warehouse>();
        }
    }
}
