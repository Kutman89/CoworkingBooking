using Application.DTOs.Room;

namespace Application.Interfaces;

public interface IRoomService
{
    Task<IReadOnlyList<RoomResponse>> ListAsync(CancellationToken ct = default);
    Task<RoomResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<RoomResponse> CreateAsync(CreateRoomRequest request, CancellationToken ct = default);
    Task<bool> UpdateAsync(Guid id, CreateRoomRequest request, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
