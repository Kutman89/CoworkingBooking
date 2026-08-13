using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class RoomRepository(AppDbContext context) : IRoomRepository
{
    public async Task<IReadOnlyList<Room>> GetAllAsync(
        CancellationToken ct = default)
    {
        return await context.Rooms
            .AsNoTracking()
            .Where(r => r.IsActive)
            .OrderBy(r => r.Name)
            .ToListAsync(ct);
    }
    public async Task<Room?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        return await context.Rooms
            .FirstOrDefaultAsync(r => r.Id == id, ct);
    }
    public async Task AddAsync(
        Room room,
        CancellationToken ct = default)
    {
        await context.Rooms
            .AddAsync(room, ct);
    }

    public void Update(Room room)
    {
        context.Rooms.Update(room);
    }

    public Task<int> SaveChangesAsync(
        CancellationToken ct = default)
    {
        return context.SaveChangesAsync(ct);
    }
}
