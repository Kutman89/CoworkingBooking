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

        public async Task<IEnumerable<Room>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Rooms
                .AsNoTracking()
                .Where(r => r.IsActive)
                .ToListAsync(cancellationToken);
        }
        public async Task<Room?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Rooms
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }
        public async Task AddAsync(Room room, CancellationToken cancellationToken = default)
        {
            await _context.Rooms
                .AddAsync(room, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task Update(Room room)
        {
            _context.Rooms.Update(room);
        }
    }
}
