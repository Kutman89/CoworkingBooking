using Domain.Entities;

namespace Application.Interfaces
{
    public interface IRoomRepository
    {
        Task TaskAsync(Room room);
        Task SaveChangeAsync();
    }
}
