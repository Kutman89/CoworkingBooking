
namespace Domain.Enums
{
    public enum RoomStatus
    {
        Available,      // доступна для бронирования
        Reserved,       // забронирована
        Occupied,       // сейчас занята
        Maintenance,    // на обслуживании/ремонте
        Cleaning,       // уборка
        Inactive        // отключена
    }
}