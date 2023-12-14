using DealProject.Domain;

namespace DealProject.Application;

public interface IDebtService
{
    Task<List<GetDebtDto>> GetAllAsync();
    Task<GetDebtDto?> GetAsync(Guid id);
    Task<Guid> LendAsync(string userlogin, LendDebtDto dto);
    Task<Guid> BorrowAsync(string userlogin, BorrowDebtDto dto);
    Task<DealStatusType?> AcceptAsync(string userlogin, AcceptDebtDto dto);
    Task<DealStatusType?> CancelAsync(string userlogin, CancelDebtDto dto);
    Task<DealStatusType?> CloseAsync(string userlogin, CloseDebtDto dto);
}