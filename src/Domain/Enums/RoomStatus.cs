
namespace Domain.Enums
{
    public enum RoomStatus
    {
        Available = 1,      // доступна для бронирования
        Reserved = 2,       // забронирована
        Occupied = 3,       // сейчас занята
        Maintenance = 4,    // на обслуживании/ремонте
        Cleaning = 5,       // уборка
        Inactive = 6        // отключена
    }
}