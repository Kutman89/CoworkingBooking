using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Security;

public sealed class PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<User> _hasher = new();

    public string Hash(string password) =>
        _hasher.HashPassword(null!, password);

    public bool Verify(string password, string hash) =>
        _hasher.VerifyHashedPassword(null!, hash, password)
        is PasswordVerificationResult.Success
        or PasswordVerificationResult.SuccessRehashNeeded;
}
