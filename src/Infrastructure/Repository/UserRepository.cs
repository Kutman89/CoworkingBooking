using Application.Interfaces;
using Infrastructure.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IReadOnlyList<User>> GetAllAsync(
        CancellationToken ct = default)
    {
        return await context.Users.
            AsNoTracking().
            OrderBy(u => u.FirstName).
            ToListAsync(ct);
    }

    public async Task Update(
        User user,
        CancellationToken ct = default)
    {
        context.Users.Update(user);
    }

    public async Task<int> SaveChangesAsync(
        CancellationToken ct = default)
    {
        return await context.SaveChangesAsync(ct);
    }
}
