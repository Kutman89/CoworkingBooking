
using Domain.Entities;

namespace Application.Interfaces;

public interface IBookingRepository
{
    Task<bool> HasOverlapAsync(
        Guid roomId,
        DateTime startUtc,
        DateTime endUtc,
        Guid? excludeBookingId = null,
        CancellationToken ct = default);
    Task<IReadOnlyList<Booking>> GetAllAsync(CancellationToken ct = default);
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Booking booking, CancellationToken ct = default);
    void Update(Booking booking);
    void Delete(Booking booking);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
