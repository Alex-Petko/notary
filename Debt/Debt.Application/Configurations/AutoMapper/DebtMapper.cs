using AutoMapper;
using DebtManager.Domain;

namespace DebtManager.Application;

internal sealed class DebtMapper : Profile
{
    public DebtMapper()
    {
        CreateMap<Debt, GetDebtQueryResult>();

        var mapExpression = CreateMap<CreateDebtCommand, Debt>();
        mapExpression.IncludeMembers(x => x.Body);

        CreateMap<CreateDebtCommandBody, Debt>();
    }
}