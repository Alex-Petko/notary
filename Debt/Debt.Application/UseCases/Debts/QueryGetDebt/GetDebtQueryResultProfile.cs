using AutoMapper;
using DebtManager.Domain;

namespace DebtManager.Application;

internal sealed class GetDebtQueryResultProfile : Profile
{
    public GetDebtQueryResultProfile()
    {
        CreateMap<Debt, GetDebtQueryResult>();
    }
}
