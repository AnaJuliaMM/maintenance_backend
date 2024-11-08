using Microsoft.EntityFrameworkCore;
using MachineAPI.Domain.Entities;
using MachineAPI.API.DTOs;
using AutoMapper;

namespace MachineAPI.API.Extensions;

public class MappingDTO : Profile
{
    public MappingDTO()
    {
        CreateMap<Machine, MachineDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Location, LocationDTO>().ReverseMap();
    }
}