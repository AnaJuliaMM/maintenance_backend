using AutoMapper;
using UserAuth.API.DTOs;
using UserAuth.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserTokenPayloadDTO>();
    }
}
