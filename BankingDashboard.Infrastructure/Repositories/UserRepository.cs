using BankingDashboard.Application.Common.Interfaces;
using BankingDashboard.Domain.Entities;
using BankingDashboard.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BankingDashboard.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public Task<List<User>> GetAllUsersAsync()
    {
        return _context.Users.ToListAsync();
    }
}
