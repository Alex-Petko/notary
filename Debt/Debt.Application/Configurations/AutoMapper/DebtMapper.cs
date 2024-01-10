using AutoMapper;
using DebtManager.Domain;
using System.Diagnostics.CodeAnalysis;

namespace DebtManager.Application;

[ExcludeFromCodeCoverage]
internal sealed class DebtMapper : Profile
{
    public DebtMapper()
    {
        CreateMap<Debt, GetDebtDto>();

        CreateMap<InitDebtRequest, Debt>();
    }
}