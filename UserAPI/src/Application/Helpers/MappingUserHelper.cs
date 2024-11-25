using AutoMapper;
using UserAPI.API.DTOs;
using UserAPI.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserTokenPayloadDTO>();
    }
}
