using Application.Common;
using Application.DTOs.Room;

namespace Application.Interfaces;

public interface IRoomService
{
    Task<PagedResult<RoomResponse>> ListAsync(RoomQueryParameters query, CancellationToken ct = default);
    Task<RoomResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<RoomResponse> CreateAsync(CreateRoomRequest request, CancellationToken ct = default);
    Task<bool> UpdateAsync(Guid id, UpdateRoomRequest request, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
