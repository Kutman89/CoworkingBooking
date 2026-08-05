using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(Room room, CancellationToken cancellationToken = default);
        Task Update(Room room);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
