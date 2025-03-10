namespace BankingDashboard.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public TransactionType Type { get; set; } // Enum for better control

    public Guid AccountId { get; set; } // Foreign key to Account
    public Account? Account { get; set; } // Navigation property

    public Guid? TargetAccountId { get; set; } // Used for transfers
    public Account? TargetAccount { get; set; } // Navigation for transfers
}

// Enum for transaction types
public enum TransactionType
{
    Deposit,
    Withdrawal,
    Transfer
}
