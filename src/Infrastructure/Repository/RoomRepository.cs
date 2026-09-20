using Application.Common;
using Application.DTOs.Room;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class RoomRepository(AppDbContext context) : IRoomRepository
{
    public async Task<PagedResult<Room>> GetPagedAsync(
        RoomQueryParameters query,
        CancellationToken ct = default)
    {
        var rooms = context.Rooms
            .AsNoTracking()
            .Where(r => r.IsActive);

        if (query.MinCapacity is { } minCapacity)
            rooms = rooms.Where(r => r.Capacity >= minCapacity);

        if(query.Type is { } type)
            rooms = rooms.Where(r => r.Type == type);

        rooms = (query.SortBy?.ToLowerInvariant(), query.SortDescending) switch
        {
            ("name", false) => rooms.OrderBy(r => r.Name),
            ("name", true) => rooms.OrderByDescending(r => r.Name),
            ("capacity", false) => rooms.OrderBy(r => r.Capacity),
            ("capacity", true) => rooms.OrderByDescending(r => r.Capacity),
            ("floor", false) => rooms.OrderBy(r => r.Floor),
            ("floor", true) => rooms.OrderByDescending(r => r.Floor),
            _ => rooms.OrderBy(r => r.Name)
        };

        var totalCount = await rooms.CountAsync(ct);

        var items = await rooms
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(ct);

        return new PagedResult<Room>(items, totalCount, query.Page, query.PageSize);
    }

    public async Task<Room?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        return await context.Rooms
            .AsNoTracking()
            .Where(r => r.IsActive)
            .FirstOrDefaultAsync(r => r.Id == id, ct);
    }

    public async Task AddAsync(
        Room room,
        CancellationToken ct = default)
    {
        await context.Rooms
            .AddAsync(room, ct);
    }

    public void Update(
        Room room)
    {
        context.Rooms.Update(room);
    }

    public Task<int> SaveChangesAsync(
        CancellationToken ct = default)
    {
        return context.SaveChangesAsync(ct);
    }
}