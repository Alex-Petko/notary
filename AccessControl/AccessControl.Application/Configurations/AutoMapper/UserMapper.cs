using AccessControl.Domain;
using AutoMapper;

namespace AccessControl.Application;

internal sealed class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<Credentials, User>();
    }
}