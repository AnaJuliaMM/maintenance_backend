using Microsoft.EntityFrameworkCore;
using MachineAPI.Domain.Entities;
using MachineAPI.API.DTOs;
using AutoMapper;

namespace MachineAPI.API.Extensions;

public class MappingDTO : Profile
{
    public MappingDTO()
    {
        CreateMap<Machine, MachineDTO>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category)) 
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));

        CreateMap<Machine, CreateUpdateMachineDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Location, LocationDTO>().ReverseMap();
    }
}