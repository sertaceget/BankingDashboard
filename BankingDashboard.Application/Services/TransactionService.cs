using BankingDashboard.Application.Common.Interfaces;
using BankingDashboard.Domain.Entities;

namespace BankingDashboard.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;

    public TransactionService(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
    }

    public async Task<Transaction> DepositAsync(Guid accountId, decimal amount)
    {
        var account = await _accountRepository.GetByIdAsync(accountId);
        if (account == null)
            throw new Exception("Account not found.");

        if (amount <= 0)
            throw new Exception("Deposit amount must be greater than zero.");

        account.UpdateBalance(amount);
        await _accountRepository.UpdateAsync(account);

        var transaction = new Transaction
        {
            AccountId = account.Id,
            Amount = amount,
            Type = TransactionType.Deposit
        };

        return await _transactionRepository.CreateAsync(transaction);
    }

    public async Task<Transaction> WithdrawAsync(Guid accountId, decimal amount)
    {
        var account = await _accountRepository.GetByIdAsync(accountId);
        if (account == null)
            throw new Exception("Account not found.");

        if (amount <= 0)
            throw new Exception("Withdrawal amount must be greater than zero.");

        if (account.Balance < amount)
            throw new Exception("Insufficient balance.");

        account.UpdateBalance(-amount);
        await _accountRepository.UpdateAsync(account);

        var transaction = new Transaction
        {
            AccountId = account.Id,
            Amount = amount,
            Type = TransactionType.Withdrawal
        };

        return await _transactionRepository.CreateAsync(transaction);
    }

    public async Task<Transaction> TransferAsync(Guid fromAccountId, Guid toAccountId, decimal amount)
    {
        var fromAccount = await _accountRepository.GetByIdAsync(fromAccountId);
        var toAccount = await _accountRepository.GetByIdAsync(toAccountId);

        if (fromAccount == null || toAccount == null)
            throw new Exception("One or both accounts not found.");

        if (amount <= 0)
            throw new Exception("Transfer amount must be greater than zero.");

        if (fromAccount.Balance < amount)
            throw new Exception("Insufficient balance.");

        fromAccount.UpdateBalance(-amount);
        toAccount.UpdateBalance(amount);

        await _accountRepository.UpdateAsync(fromAccount);
        await _accountRepository.UpdateAsync(toAccount);

        var transaction = new Transaction
        {
            AccountId = fromAccount.Id,
            Amount = amount,
            Type = TransactionType.Transfer
        };

        return await _transactionRepository.CreateAsync(transaction);
    }

    public async Task<List<Transaction>> GetTransactionsByAccountIdAsync(Guid accountId)
    {
        return await _transactionRepository.GetByAccountIdAsync(accountId);
    }
}
