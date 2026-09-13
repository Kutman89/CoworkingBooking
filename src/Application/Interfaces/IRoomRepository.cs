using Application.Common;
using Application.DTOs.Room;
using Domain.Entities;

namespace Application.Interfaces;

public interface IRoomRepository
{
    Task<PagedResult<Room>> GetPagedAsync(RoomQueryParameters query, CancellationToken ct = default);
    Task<Room?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Room room, CancellationToken ct = default);
    void Update(Room room);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
