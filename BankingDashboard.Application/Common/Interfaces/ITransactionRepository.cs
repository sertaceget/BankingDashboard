using BankingDashboard.Domain.Entities;

namespace BankingDashboard.Application.Common.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction?> GetByIdAsync(Guid id);
    Task<List<Transaction>> GetByAccountIdAsync(Guid accountId);
    Task<Transaction> CreateAsync(Transaction transaction);
}
