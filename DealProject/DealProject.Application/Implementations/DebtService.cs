using AutoMapper;
using DealProject.Domain;
using DealProject.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DealProject.Application;

internal class DebtService : IDebtService, IDisposable, IAsyncDisposable
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public DebtService(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<GetDebtDto>> GetAllAsync()
    {
        var debts = await _repository.Debts.GetAsNoTracking().ToListAsync();
        var getDebtDtos = new List<GetDebtDto>(debts.Count);

        _mapper.Map(debts, getDebtDtos);

        return getDebtDtos;
    }

    public async Task<GetDebtDto?> GetAsync(Guid id)
    {
        var debt = await _repository.Debts.FindAsync(id);
        return debt != null ? _mapper.Map<GetDebtDto>(debt) : null;
    }


    public async Task<Guid> LendAsync(string userlogin, LendDebtDto dto)
    {
        var debt = _mapper.Map<Debt>(dto);
        debt.LenderLogin = userlogin;
        debt.BorrowerLogin = dto.Login;
        debt.Status = DealStatusType.LenderApproved;

        _repository.Debts.Add(debt);

        await _repository.SaveChangesAsync();

        return debt.Id;
    }

    public async Task<Guid> BorrowAsync(string userlogin, BorrowDebtDto dto)
    {
        var debt = _mapper.Map<Debt>(dto);
        debt.LenderLogin = dto.Login;
        debt.BorrowerLogin = userlogin;
        debt.Status = DealStatusType.BorrowerApproved;

        _repository.Debts.Add(debt);

        await _repository.SaveChangesAsync();

        return debt.Id;
    }

    public async Task<DealStatusType?> AcceptAsync(string userlogin, AcceptDebtDto dto)
    {
        var debt = await _repository.Debts.FindAsync(dto.DebtId);
        if (debt == null)
            return null;

        var tempStatus = debt.Status;

        if (debt.LenderLogin == userlogin)
        {
            debt.Status = debt.Status switch
            {
                DealStatusType.BorrowerApproved => DealStatusType.Opened,
                _ => debt.Status,
            };
        }

        if (debt.BorrowerLogin == userlogin)
        {
            debt.Status = debt.Status switch
            {
                DealStatusType.LenderApproved => DealStatusType.Opened,
                _ => debt.Status,
            };
        }

        if (tempStatus != debt.Status)
        {
            await _repository.SaveChangesAsync();
        }

        return debt.Status;
    }

    public async Task<DealStatusType?> CancelAsync(string userlogin, CancelDebtDto dto)
    {
        var debt = await _repository.Debts.FindAsync(dto.DebtId);
        if (debt == null)
            return null;

        var tempStatus = debt.Status;

        if (debt.LenderLogin == userlogin)
        {
            debt.Status = debt.Status switch
            {
                DealStatusType.LenderApproved => DealStatusType.LenderCanceled,
                DealStatusType.BorrowerApproved => DealStatusType.LenderCanceled,
                DealStatusType.BorrowerCanceled => DealStatusType.Canceled,
                _ => debt.Status,
            };
        }

        if (debt.BorrowerLogin == userlogin)
        {
            debt.Status = debt.Status switch
            {
                DealStatusType.LenderApproved => DealStatusType.BorrowerCanceled,
                DealStatusType.BorrowerApproved => DealStatusType.BorrowerCanceled,
                DealStatusType.LenderCanceled => DealStatusType.Canceled,
                _ => debt.Status,
            };
        }

        if (tempStatus != debt.Status)
        {
            await _repository.SaveChangesAsync();
        }

        return debt.Status;
    }

    public async Task<DealStatusType?> CloseAsync(string userlogin, CloseDebtDto dto)
    {
        var debt = await _repository.Debts.FindAsync(dto.DebtId);
        if (debt == null)
            return null;

        var tempStatus = debt.Status;
        if (debt.LenderLogin == userlogin)
        {
            debt.Status = debt.Status switch
            {
                DealStatusType.Opened => DealStatusType.LenderClosed,
                DealStatusType.BorrowerClosed => DealStatusType.Closed,
                _ => debt.Status,
            };
        }

        if (debt.BorrowerLogin == userlogin)
        {
            debt.Status = debt.Status switch
            {
                DealStatusType.Opened => DealStatusType.BorrowerClosed,
                DealStatusType.LenderClosed => DealStatusType.Closed,
                _ => debt.Status,
            };
        }

        if (tempStatus != debt.Status)
        {
            await _repository.SaveChangesAsync();
        }

        return debt.Status;
    }

    public void Dispose()
    {
        _repository.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _repository.DisposeAsync();
    }

}
