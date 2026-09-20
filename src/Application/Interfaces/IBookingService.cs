using Application.Common;
using Application.DTOs.Booking;

namespace Application.Interfaces;

public interface IBookingService
{
    Task<PagedResult<BookingResponse>> ListAsync(BookingQueryParameters query, CancellationToken ct = default);
    Task<BookingResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<BookingResponse> CreateAsync(CreateBookingRequest request, CancellationToken ct = default);
    Task<bool> UpdateTimeAsync(Guid id, UpdateBookingTimeRequest request, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
