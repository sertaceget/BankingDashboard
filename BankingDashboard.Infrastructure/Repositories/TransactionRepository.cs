using BankingDashboard.Application.Common.Interfaces;
using BankingDashboard.Domain.Entities;
using BankingDashboard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingDashboard.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Transaction?> GetByIdAsync(Guid id)
    {
        return await _context.Transactions.Include(t => t.Account)
                                          .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Transaction>> GetByAccountIdAsync(Guid accountId)
    {
        return await _context.Transactions.Where(t => t.AccountId == accountId)
                                          .ToListAsync();
    }

    public async Task<Transaction> CreateAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }
}
