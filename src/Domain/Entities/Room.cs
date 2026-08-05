using Domain.Enums;

namespace Domain.Entities
{
    public class Room
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public int Floor { get; set; }
        public RoomType Type { get; set; }
        public bool IsActive { get; set; }
    }
}
