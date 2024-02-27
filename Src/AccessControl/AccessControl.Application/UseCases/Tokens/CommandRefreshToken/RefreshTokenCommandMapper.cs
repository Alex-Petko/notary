using AutoMapper;

namespace AccessControl.Application;

internal sealed class RefreshTokenCommandMapper : Profile
{
    public RefreshTokenCommandMapper()
    {
        CreateMap<RefreshTokenCommand, TokenManagerDto>();
    }
}
