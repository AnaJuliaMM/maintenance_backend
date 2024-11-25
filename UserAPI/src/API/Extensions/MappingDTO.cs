using Microsoft.EntityFrameworkCore;
using UserAPI.Domain.Entities;
using UserAPI.API.DTOs;
using AutoMapper;

namespace UserAPI.API.Extensions;

public class MappingDTO : Profile
{
    public MappingDTO()
    {
        CreateMap<User, UserDTO>().ReverseMap();
    }
}