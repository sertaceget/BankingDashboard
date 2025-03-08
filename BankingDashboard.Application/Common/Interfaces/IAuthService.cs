﻿namespace BankingDashboard.Application.Common.Interfaces;

public interface IAuthService
{
    Task<string?> AuthenticateAsync(string email, string password);
    string HashPassword(string password);
}
