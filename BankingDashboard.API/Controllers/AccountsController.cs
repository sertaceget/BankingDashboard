using BankingDashboard.Application.Common.Interfaces;
using BankingDashboard.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BankingDashboard.Application.DTOs;

namespace BankingDashboard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Require authentication
public class AccountsController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountsController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAccount()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized("Invalid token.");

        var account = await _accountService.CreateAccountAsync(Guid.Parse(userId));
        return CreatedAtAction(nameof(GetAccount), new { id = account.Id }, account);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccount(Guid id)
    {
        var account = await _accountService.GetAccountByIdAsync(id);
        if (account == null)
            return NotFound();

        var accountDto = new AccountDto
        {
            Id = account.Id,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            UserId = account.UserId,
            Transactions = account.Transactions?.Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Timestamp = t.Timestamp,
                Type = (int)t.Type,
                AccountId = t.AccountId,
                TargetAccountId = t.TargetAccountId
            }).ToList() ?? new List<TransactionDto>()
        };

        return Ok(accountDto);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")] // Only admins can access this
    public async Task<IActionResult> GetAllAccounts()
    {
        var accounts = await _accountService.GetAllAccountsAsync();
        return Ok(accounts);
    }
}
