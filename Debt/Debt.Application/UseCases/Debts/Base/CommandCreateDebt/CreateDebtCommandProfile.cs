using AutoMapper;
using DebtManager.Domain;

namespace DebtManager.Application;

internal sealed class CreateDebtCommandProfile : Profile
{
    public CreateDebtCommandProfile()
    {
        CreateMap<CreateDebtCommand, Debt>()
            .IncludeMembers(x => x.Body);

        CreateMap<CreateDebtCommandBody, Debt>();
    }
}