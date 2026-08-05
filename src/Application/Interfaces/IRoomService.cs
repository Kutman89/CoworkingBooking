using Application.DTOs.Room;

namespace Application.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomResponse>> ListAsync(CancellationToken cancellationToken = default);
        Task<RoomResponse?> GetRoomByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Guid> CreateRoomAsync(CreateRoomRequest request, CancellationToken cancellationToken = default);
        Task<bool> UpdateRoomAsync(Guid id, CreateRoomRequest request, CancellationToken cancellationToken = default);
        Task<bool> DeleteRoomAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
