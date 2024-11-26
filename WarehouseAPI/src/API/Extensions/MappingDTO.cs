using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WarehouseAPI.API.DTOs;
using WarehouseAPI.Domain.Entities;

namespace WarehouseAPI.API.Extensions;

public class MappingDTO : Profile
{
    public MappingDTO()
    {
        CreateMap<Item, ItemDTO>().ReverseMap();
    }
}
