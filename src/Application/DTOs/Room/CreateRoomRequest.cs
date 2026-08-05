using Domain.Enums;

namespace Application.DTOs.Room
{
    public class CreateRoomRequest
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int Capacity { get; set; }

        public int Floor { get; set; }

        public RoomType Type { get; set; }
    }
}
