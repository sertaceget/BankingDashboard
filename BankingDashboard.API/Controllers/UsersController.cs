using BankingDashboard.API.Models;
using BankingDashboard.Application.Common.Interfaces;
using BankingDashboard.Application.DTOs;
using BankingDashboard.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingDashboard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public UsersController(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 8)
        {
            return BadRequest("Password must be at least 8 characters long.");
        }

        var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return BadRequest("User already exists.");
        }

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = _authService.HashPassword(request.Password),
            Role = request.Role ?? "User" // Default role is User
        };

        var newUser = await _userRepository.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUser), new { email = newUser.Email }, new UserResponse(newUser));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var token = await _authService.AuthenticateAsync(request.Email, request.Password);
        if (token == null)
        {
            return Unauthorized("Invalid credentials.");
        }

        return Ok(new { Token = token });
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetUser(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null)
            return NotFound();

        var userDto = new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            Role = user.Role,
            Accounts = user.Accounts?.Select(a => new AccountDto
            {
                Id = a.Id,
                AccountNumber = a.AccountNumber,
                Balance = a.Balance,
                UserId = a.UserId
                // Typically don't include transactions when getting a user
            }).ToList() ?? new List<AccountDto>()
        };

        return Ok(userDto);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsersAsync();
        return Ok(users.Select(user => new UserResponse(user)));
    }
}
