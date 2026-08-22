using Application.DTOs.Booking;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public sealed class BookingService(IBookingRepository repository) : IBookingService
{
    public async Task<IReadOnlyList<BookingResponse>> ListAsync(
        CancellationToken ct = default)
    {
        var bookings = await repository.GetAllAsync(ct);
        return bookings.Select(MapToResponse).ToList();
    }

    public async Task<BookingResponse?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        var booking = await repository.GetByIdAsync(id, ct);
        return booking == null ? null : MapToResponse(booking);
    }

    public async Task<BookingResponse> CreateAsync(
        CreateBookingRequest request,
        CancellationToken ct = default)
    {
        var booking = Booking.Create(
            request.UserId,
            request.RoomId,
            request.StartTime,
            request.EndTime);

        await repository.AddAsync(booking, ct);
        await repository.SaveChangesAsync(ct);

        return MapToResponse(booking);
    }

    public async Task<bool> UpdateTimeAsync(
        Guid id,
        UpdateBookingTimeRequest request,
        CancellationToken ct = default)
    {
        var booking = await repository.GetByIdAsync(id, ct);

        if (booking is null) return false;

        booking.UpdateBookingTimes(request.StartTime, request.EndTime);

        repository.Update(booking);
        await repository.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken ct = default)
    {
        var booking = await repository.GetByIdAsync(id, ct);

        if(booking is null) return false;

        repository.Delete(booking);
        await repository.SaveChangesAsync(ct);

        return true;
    }

    private static BookingResponse MapToResponse(Booking booking) =>
        new(
            booking.Id,
            booking.RoomId,
            booking.UserId,
            booking.StartTime,
            booking.EndTime,
            booking.Status
            );
}
