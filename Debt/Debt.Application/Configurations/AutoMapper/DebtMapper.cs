using AutoMapper;
using DebtManager.Domain;

namespace DebtManager.Application;

internal sealed class DebtMapper : Profile
{
    public DebtMapper()
    {
        CreateMap<Debt, GetDebtQueryResult>();

        CreateMap<CreateDebtCommand, Debt>();
    }
}