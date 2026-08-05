using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllAsync();
        Task<Room?> GetByIdAsync(Guid id);
        Task AddAsync(Room room);
        Task UpdateAsync(Room room);
        Task SaveChangesAsync();
    }
}
