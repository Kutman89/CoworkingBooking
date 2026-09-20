using Application.Common;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.DTOs.User;
namespace Infrastructure.Repository;

public sealed class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task AddAsync(
        User user,
        CancellationToken ct = default)
    {
        await context.Users.
            AddAsync(user, ct);
    }

    public async Task<User?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        return await context.Users.
            FirstOrDefaultAsync(u => u.Id == id, ct);
    }

    public async Task<PagedResult<User>> GetPagedAsync(
        UserQueryParameters query,
        CancellationToken ct = default)
    {
        var users = context.Users
            .AsNoTracking().
            Where(u => u.IsBlocked);

        users = (query.SortBy?.ToLowerInvariant(), query.SortDescending) switch
        {
            ("firstname", true) => users.OrderByDescending(u => u.FirstName),
            ("firstname", false) => users.OrderBy(u => u.FirstName),
            ("lastname", true) => users.OrderByDescending(u => u.LastName),
            ("lastname", false) => users.OrderBy(u => u.LastName),
            ("email", true) => users.OrderByDescending(u => u.Email),
            ("email", false) => users.OrderBy(u => u.Email),
            _ => users.OrderBy(u => u.Id)
        };

        var totalCount = await users.CountAsync(ct);

        var items = await users
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(ct);

        return new PagedResult<User>(items, totalCount, query.Page, query.PageSize);
    }

    public void Update(
        User user)
    {
        context.Users.Update(user);
    }

    public async Task<int> SaveChangesAsync(
        CancellationToken ct = default)
    {
        return await context.SaveChangesAsync(ct);
    }
}
