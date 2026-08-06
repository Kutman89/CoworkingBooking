using Application.DTOs.Room;

namespace Application.Interfaces;

public interface IRoomService
{
    Task<IReadOnlyList<RoomResponse>> ListAsync(CancellationToken cancellationToken = default);
    Task<RoomResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<RoomResponse> CreateAsync(CreateRoomRequest request, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Guid id, CreateRoomRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
