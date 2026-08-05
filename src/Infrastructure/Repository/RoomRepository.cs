using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _context;

        public RoomRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAllAsync()
        {
            return await _context.Rooms.Where(r => r.IsActive).ToListAsync();
        }
        public async Task<Room?> GetByIdAsync(Guid id)
        {
            return await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task AddAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Room room)
        {
            _context.Rooms.Update(room);
        }
    }
}
