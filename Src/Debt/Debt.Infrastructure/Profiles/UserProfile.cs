using AccessControl.Client;
using AutoMapper;
using DebtManager.Domain;

namespace DebtManager.Infrastructure;

internal sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<GetUserQuery, User>();
    }
}