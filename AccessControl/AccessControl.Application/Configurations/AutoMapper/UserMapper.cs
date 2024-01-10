using AccessControl.Domain;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace AccessControl.Application;

[ExcludeFromCodeCoverage]
internal sealed class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<RequestBase, User>().ForMember(user => user.PasswordHash, request => request.MapFrom(z => z.Password));
    }
}