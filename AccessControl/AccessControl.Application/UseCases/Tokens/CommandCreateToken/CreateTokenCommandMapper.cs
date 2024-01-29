using AutoMapper;

namespace AccessControl.Application;

internal sealed class CreateTokenCommandMapper : Profile
{
    public CreateTokenCommandMapper()
    {
        CreateMap<CreateTokenCommand, AuthenticationDto>();
        CreateMap<CreateTokenCommand, TokenManagerDto>();
    }
}
