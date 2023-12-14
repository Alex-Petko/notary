using AutoMapper;
using DealProject.Domain;
using System.Diagnostics.CodeAnalysis;

namespace DealProject.Application;

[ExcludeFromCodeCoverage]
internal class DebtMapper : Profile
{
    public DebtMapper()
    {
        CreateMap<Debt, GetDebtDto>();
        CreateMap<LendDebtDto, Debt>()
            .AfterMap((source, destination) 
                => destination.Begin = DateTime.SpecifyKind(destination.Begin, DateTimeKind.Utc));

        CreateMap<BorrowDebtDto, Debt>()
            .AfterMap((source, destination)
                => destination.Begin = DateTime.SpecifyKind(destination.Begin, DateTimeKind.Utc));
    }
}
