using Application.DTOs.Room;
using Domain.Entities;
namespace Application.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> ListAsync();
        Task<Room?> GetRoomByIdAsync(Guid id);
        Task CreateRoomAsync(CreateRoomRequest request);
        Task UpdateRoomAsync(Guid id, CreateRoomRequest request);
        Task DeleteRoomAsync(Guid id);
    }
}
