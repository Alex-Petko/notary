using AutoMapper;
using DebtManager.Domain;

namespace DebtManager.Application;

internal sealed class CreateDebtCommandProfile : Profile
{
    public CreateDebtCommandProfile()
    {
        CreateMap<CreateDebtCommand, Debt>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.LenderLogin, opt => opt.Ignore())
            .ForMember(x => x.BorrowerLogin, opt => opt.Ignore())
            .ForMember(x => x.Status, opt => opt.Ignore())
            .ForSourceMember(x => x.Login, opt => opt.DoNotValidate())
            .IncludeMembers(x => x.Body);

        CreateMap<CreateDebtCommandBody, Debt>()
            .ForMember(x => x.Id, opt => opt.Ignore())
             .ForMember(x => x.LenderLogin, opt => opt.Ignore())
             .ForMember(x => x.BorrowerLogin, opt => opt.Ignore())
             .ForMember(x => x.Status, opt => opt.Ignore());
    }
}