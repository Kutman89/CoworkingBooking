using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs.Room;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repository;

        public RoomService(IRoomRepository repository)
        {
            _repository = repository;
        }

        // создать комнату
        public async Task CreateRoomAsync(CreateRoomRequest request)
        {
            var room = new Room
            {
                Name = request.Name,
                Description = request.Description,
                Capacity = request.Capacity,
                Floor = request.Floor,
                Type = request.Type,
                IsActive = true
            };
            await _repository.AddAsync(room);
            await _repository.SaveChangesAsync();
        }

        // получить все комнаты
        public async Task<IEnumerable<Room>> ListAsync()
        {
            return await _repository.GetAllAsync();
        }

        // получить по айди
        public async Task<Room?> GetRoomByIdAsync(Guid id)
        {
            var room = await _repository.GetByIdAsync(id);
            if(room == null)
            {
                throw new KeyNotFoundException($"Room with id {id} not found");
            }
            return room;
        }

        // обновить комнату
        public async Task UpdateRoomAsync(Guid id, CreateRoomRequest request)
        {
            var room = await _repository.GetByIdAsync(id);
            if(room == null)
            {
                throw new KeyNotFoundException($"Room with id {id} not found");
            }

            room.Name = request.Name;
            room.Description = request.Description;
            room.Capacity = request.Capacity;
            room.Floor = request.Floor;
            room.Type = request.Type;

            await _repository.UpdateAsync(room);
            await _repository.SaveChangesAsync();
        }

        // удалить комнату
        public async Task DeleteRoomAsync(Guid id)
        {
            var room = await _repository.GetByIdAsync(id);

            if (room == null)
            {
                throw new KeyNotFoundException($"Room with id {id} not found");
            }

            room.IsActive = false;

            await _repository.UpdateAsync(room);
            await _repository.SaveChangesAsync();
        }

    }
}
