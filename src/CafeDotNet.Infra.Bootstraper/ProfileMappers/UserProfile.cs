using AutoMapper;
using CafeDotNet.Core.Users.DTOs;
using CafeDotNet.Core.Users.Entities;

namespace CafeDotNet.Infra.Bootstraper.ProfileMappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, AuthenticationResponse>();
    }
}
