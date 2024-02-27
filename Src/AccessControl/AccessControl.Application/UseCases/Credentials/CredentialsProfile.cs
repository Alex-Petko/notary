using AccessControl.Domain;
using AutoMapper;

namespace AccessControl.Application;

internal sealed class CredentialsProfile : Profile
{
    public CredentialsProfile()
    {
        CreateMap<Credentials, User>();
    }
}