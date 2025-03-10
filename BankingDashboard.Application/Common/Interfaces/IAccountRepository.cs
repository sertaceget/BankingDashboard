using BankingDashboard.Domain.Entities;

namespace BankingDashboard.Application.Common.Interfaces;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(Guid id);
    Task<Account?> GetByAccountNumberAsync(string accountNumber);
    Task<List<Account>> GetByUserIdAsync(Guid userId);
    Task<Account> CreateAsync(Account account);
    Task UpdateAsync(Account account);
    Task<List<Account>> GetAllAsync();
}
