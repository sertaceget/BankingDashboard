using System.Security.Principal;

namespace BankingDashboard.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;  // Hashed password
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<Account> Accounts { get; set; } = new();
    public string Role { get; set; } = "User";
}
