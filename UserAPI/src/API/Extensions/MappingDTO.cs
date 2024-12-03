using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserAPI.API.DTOs;
using UserAPI.Domain.Entities;

namespace UserAPI.API.Extensions;

public class MappingDTO : Profile
{
    public MappingDTO()
    {
        CreateMap<Role, RoleDTO>().ReverseMap();

        CreateMap<User, UserTokenPayloadDTO>();

        CreateMap<User, CreateUpdateUserDTO>().ReverseMap();

        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
    }
}
