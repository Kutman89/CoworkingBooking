using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class RoomRepository(AppDbContext context) : IRoomRepository
{
    public async Task<IReadOnlyList<Room>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await context.Rooms
            .AsNoTracking()
            .Where(r => r.IsActive)
            .OrderBy(r => r.Name)
            .ToListAsync(cancellationToken);
    }
    public async Task<Room?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await context.Rooms
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
    public async Task AddAsync(
        Room room,
        CancellationToken cancellationToken = default)
    {
        await context.Rooms
            .AddAsync(room, cancellationToken);
    }

    public void Update(Room room)
    {
        context.Rooms.Update(room);
    }

    public Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}
