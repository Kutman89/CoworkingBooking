using Application.DTOs.Room;
using Domain.Entities;
namespace Application.Interfaces
{
    public interface IRoomService
    {
        Task CreateAsync(CreateRoomRequest request);
    }
}
