namespace BankingDashboard.Domain.Entities;

public class Account
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; } = 0.0m;
    public Guid UserId { get; set; } // Foreign key to User
    public User? User { get; set; } // Navigation property
    public List<Transaction> Transactions { get; set; } = [];
}
