using BankingDashboard.Domain.Entities;

namespace BankingDashboard.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User> CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task<List<User>> GetAllUsersAsync();
}
