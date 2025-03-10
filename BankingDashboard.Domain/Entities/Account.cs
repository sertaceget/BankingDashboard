namespace BankingDashboard.Domain.Entities;

public class Account
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; private set; } = 0.0m; // Private setter to ensure controlled balance updates
    public Guid UserId { get; set; } // Foreign key to User
    public User? User { get; set; } // Navigation property
    public List<Transaction> Transactions { get; set; } = [];

    // Methods to enforce business rules
    public void Deposit(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Deposit amount must be greater than zero.");
        Balance += amount;
    }

    public bool Withdraw(decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Withdrawal amount must be greater than zero.");
        if (amount > Balance) return false; // Insufficient funds
        Balance -= amount;
        return true;
    }

    // Add a method to update balance safely
    public void UpdateBalance(decimal amount)
    {
        Balance += amount;
    }
}
