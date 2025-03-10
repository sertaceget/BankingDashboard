using BankingDashboard.Application.Common.Interfaces;
using BankingDashboard.Domain.Entities;

namespace BankingDashboard.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<Account?> GetAccountByIdAsync(Guid accountId)
    {
        return await _accountRepository.GetByIdAsync(accountId);
    }

    public async Task<List<Account>> GetAccountsByUserIdAsync(Guid userId)
    {
        return await _accountRepository.GetByUserIdAsync(userId);
    }

    public async Task<Account> CreateAccountAsync(Guid userId)
    {
        var newAccount = new Account
        {
            UserId = userId,
            AccountNumber = Guid.NewGuid().ToString().Substring(0, 10) // Generate a simple account number
        };

        return await _accountRepository.CreateAsync(newAccount);
    }

    public async Task<List<Account>> GetAllAccountsAsync()
    {
        // Implement logic to get all accounts
        return await _accountRepository.GetAllAsync();
    }
}
