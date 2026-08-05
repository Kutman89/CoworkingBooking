using Application.DTOs.Room;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }


        public async Task CreateAsync(CreateRoomRequest request)
        {
            var room = new Room
            {
                Name = request.Name,
                Description = request.Description,
                Capacity = request.Capacity,
                Floor = request.Floor,
                Type = request.Type
            };
            await _roomRepository.TaskAsync(room);
            await _roomRepository.SaveChangeAsync();
        }
    }
}
