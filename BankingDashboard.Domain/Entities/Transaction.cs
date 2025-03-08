namespace BankingDashboard.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string TransactionType { get; set; } = string.Empty; // "Deposit", "Withdrawal", "Transfer"

    public Guid AccountId { get; set; } // Foreign key to Account
    public Account? Account { get; set; } // Navigation property
}
