using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _context;

        public RoomRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task TaskAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
