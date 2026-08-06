using Domain.Entities;

namespace Application.Interfaces;

public interface IRoomRepository
{
    Task<IReadOnlyList<Room>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Room room, CancellationToken cancellationToken = default);
    void Update(Room room);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
