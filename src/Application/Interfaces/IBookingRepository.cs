
using Domain.Entities;

namespace Application.Interfaces;

public interface IBookingRepository
{
    Task<IReadOnlyList<Booking>> GetAllAsync(CancellationToken ct = default);
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Booking booking, CancellationToken ct = default);
    void Update(Booking booking, CancellationToken ct = default);
    void Delete(Booking booking, CancellationToken ct = default);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
