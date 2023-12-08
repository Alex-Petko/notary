using AutoMapper;

namespace AuthService.Domain;

internal class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<CreateUserDto, User>();
        CreateMap<CreateTokenDto, User>();
    }
}
