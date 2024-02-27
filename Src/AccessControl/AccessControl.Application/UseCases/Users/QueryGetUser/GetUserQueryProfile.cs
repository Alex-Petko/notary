using AccessControl.Domain;
using AutoMapper;

namespace AccessControl.Application;

internal sealed class GetUserQueryProfile : Profile
{
    public GetUserQueryProfile()
    {
        CreateMap<User, GetUserQueryResult>();
    }
}