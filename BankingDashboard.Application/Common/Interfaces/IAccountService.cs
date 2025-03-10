using BankingDashboard.Domain.Entities;

namespace BankingDashboard.Application.Common.Interfaces;

public interface IAccountService
{
    Task<Account?> GetAccountByIdAsync(Guid accountId);
    Task<List<Account>> GetAccountsByUserIdAsync(Guid userId);
    Task<Account> CreateAccountAsync(Guid userId);
    Task<List<Account>> GetAllAccountsAsync();
}
