using AutoMapper;
using DealProject.Domain;
using System.Diagnostics.CodeAnalysis;

namespace DealProject.Application;

[ExcludeFromCodeCoverage]
internal sealed class DebtMapper : Profile
{
    public DebtMapper()
    {
        CreateMap<Debt, GetDebtDto>();

        CreateMap<LendDebtDto, Debt>();

        CreateMap<BorrowDebtDto, Debt>();
    }
}