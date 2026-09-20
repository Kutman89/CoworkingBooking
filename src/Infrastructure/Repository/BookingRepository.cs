using Application.Common;
using Application.DTOs.Booking;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
            .Where(b => b.StartTime < endUtc && b.EndTime > startUtc);

        if (excludeBookingId.HasValue) 
        {
            query = query.Where(b => b.Id != excludeBookingId.Value);
        }

        return await query.AnyAsync();
    }
    public async Task<PagedResult<Booking>> GetPagedAsync(
        BookingQueryParameters query,
        CancellationToken ct = default) 
    {
        var bookings = context.Bookings.AsNoTracking();

        if(query.RoomId is { } roomId)
            bookings = bookings.Where(b => b.RoomId == roomId);

        if(query.UserId is { } userId)
            bookings = bookings.Where(b => b.UserId == userId);

        if(query.Status is { } status)
            bookings = bookings.Where(b => b.Status == status);

        bookings = (query.SortBy?.ToLowerInvariant(), query.SortDescending) switch
        {
            ("starttime", false) => bookings.OrderBy(b => b.StartTime),
            ("starttime", true) => bookings.OrderByDescending(b => b.StartTime),
            ("endtime", false) => bookings.OrderBy(b => b.EndTime),
            ("endtime", true) => bookings.OrderByDescending(b => b.EndTime),
            _ => bookings.OrderBy(b => b.StartTime)
        };
        var totalCount = await bookings.CountAsync(ct);

        var items = await bookings
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(ct);

        return new PagedResult<Booking>(items, totalCount, query.Page, query.PageSize);
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
