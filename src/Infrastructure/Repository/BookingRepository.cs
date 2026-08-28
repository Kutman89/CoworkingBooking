using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Enums;

namespace Infrastructure.Repository;

public class BookingRepository(AppDbContext context) : IBookingRepository
{
    public async Task<bool> HasOverlapAsync(
        Guid roomId,
        DateTime startUtc,
        DateTime endUtc,
        Guid? excludeBookingId = null,
        CancellationToken ct = default)
    {
        var query = context.Bookings
            .Where(b => b.RoomId == roomId)
            .Where(b => b.Status != BookingStatus.Cancelled)
            .Where(b => b.StartTime < endUtc && startUtc > b.EndTime);

        if (excludeBookingId.HasValue) 
        {
            query = query.Where(b => b.Id != excludeBookingId.Value);
        }

        return await query.AnyAsync();
    }
    public async Task<IReadOnlyList<Booking>> GetAllAsync(
        CancellationToken ct = default) 
    {
        return await context.Bookings
            .AsNoTracking()
            .OrderBy(b => b.Id)
            .ToListAsync(ct);
    }

    public async Task<Booking?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default) 
    {
        return await context.Bookings
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id, ct);
    }

    public async Task AddAsync(
        Booking booking,
        CancellationToken ct = default) 
    {
        await context.Bookings
            .AddAsync(booking, ct);
    }

    public void Update(
        Booking booking) 
    {
        context.Bookings
            .Update(booking);
    }

    public void Delete(
        Booking booking)
    {
        context.Bookings
            .Remove(booking);
    }

    public async Task<int> SaveChangesAsync(
        CancellationToken ct = default) 
    {
        return await context.SaveChangesAsync(ct);
    }
}
