using BankingDashboard.Application.Common.Interfaces;
using BankingDashboard.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingDashboard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Require authentication
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet("{accountId:guid}")]
    public async Task<IActionResult> GetTransactions(Guid accountId)
    {
        var transactions = await _transactionService.GetTransactionsByAccountIdAsync(accountId);
        return Ok(transactions);
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] TransactionRequest request)
    {
        var transaction = await _transactionService.DepositAsync(request.AccountId, request.Amount);
        if (transaction == null)
            return BadRequest("Deposit failed.");

        return Ok("Deposit successful.");
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] TransactionRequest request)
    {
        var transaction = await _transactionService.WithdrawAsync(request.AccountId, request.Amount);
        if (transaction == null)
            return BadRequest("Insufficient funds.");

        return Ok("Withdrawal successful.");
    }

    [HttpPost("transfer")]
    public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
    {
        var transaction = await _transactionService.TransferAsync(request.FromAccountId, request.ToAccountId, request.Amount);
        if (transaction == null)
            return BadRequest("Transfer failed.");

        return Ok("Transfer successful.");
    }
}
