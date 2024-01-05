using AuthService.Domain;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace AuthService.Application;

[ExcludeFromCodeCoverage]
internal sealed class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<CreateUserDto, User>();
        CreateMap<CreateTokenDto, User>();
    }
}