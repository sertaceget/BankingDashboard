namespace BankingDashboard.API.Models;

public class TransactionRequest
{
    public Guid AccountId { get; set; }
    public decimal Amount { get; set; }
}
