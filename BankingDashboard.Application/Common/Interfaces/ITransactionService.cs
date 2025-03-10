using BankingDashboard.Domain.Entities;

namespace BankingDashboard.Application.Common.Interfaces;

public interface ITransactionService
{
    Task<Transaction> DepositAsync(Guid accountId, decimal amount);
    Task<Transaction> WithdrawAsync(Guid accountId, decimal amount);
    Task<Transaction> TransferAsync(Guid fromAccountId, Guid toAccountId, decimal amount);
    Task<List<Transaction>> GetTransactionsByAccountIdAsync(Guid accountId);
}
