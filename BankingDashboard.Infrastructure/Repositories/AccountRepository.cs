using BankingDashboard.Application.Common.Interfaces;
using BankingDashboard.Domain.Entities;
using BankingDashboard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingDashboard.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Account?> GetByIdAsync(Guid id)
    {
        return await _context.Accounts.Include(a => a.Transactions)
                                      .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Account?> GetByAccountNumberAsync(string accountNumber)
    {
        return await _context.Accounts.Include(a => a.Transactions)
                                      .FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
    }

    public async Task<List<Account>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Accounts.Where(a => a.UserId == userId)
                                      .Include(a => a.Transactions)
                                      .ToListAsync();
    }

    public async Task<Account> CreateAsync(Account account)
    {
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        return account;
    }

    public async Task UpdateAsync(Account account)
    {
        _context.Accounts.Update(account);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Account>> GetAllAsync()
    {
        return await _context.Accounts.ToListAsync();
    }
}
