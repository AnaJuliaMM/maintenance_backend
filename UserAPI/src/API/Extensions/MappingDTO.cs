using AutoMapper;
using UserAPI.API.DTOs;
using UserAPI.Domain.Entities;

namespace UserAPI.API.Extensions;

public class MappingDTO : Profile
{
    public MappingDTO()
    {
        CreateMap<Role, RoleDTO>().ReverseMap();

        CreateMap<User, CreateUpdateUserDTO>().ReverseMap();

        CreateMap<User, UserTokenPayloadDTO>()
            .ForMember(
                dest => dest.Role,
                opt =>
                    opt.MapFrom(src => src.Role != null ? "user:" + src.Role.Name : "user:viewer")
            );

        CreateMap<User, UserDTO>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role));
    }
}
