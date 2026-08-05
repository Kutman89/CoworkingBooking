using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs.Room;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _repository;

    public RoomService(IRoomRepository repository)
    {
        _repository = repository;
    }

    // создать комнату
    public async Task<Guid> CreateRoomAsync(CreateRoomRequest request, CancellationToken cancellationToken = default)
    {
        var room = new Room(
            request.Name,
            request.Description,
            request.Capacity,
            request.Floor,
            request.Type
        );
        await _repository.AddAsync(room, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return room.Id;
    }

    // получить все комнаты
    public async Task<IEnumerable<RoomResponse>> ListAsync(CancellationToken cancellationToken = default)
    {
        var rooms = await _repository.GetAllAsync(cancellationToken);
        return rooms.Select(MapToResponse);
    }

    // получить по айди
    public async Task<RoomResponse?> GetRoomByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var room = await _repository.GetByIdAsync(id, cancellationToken);
        return room == null ? null : MapToResponse(room);
    }

    // обновить комнату
    public async Task<bool> UpdateRoomAsync(Guid id, CreateRoomRequest request, CancellationToken cancellationToken = default)
    {
        var room = await _repository.GetByIdAsync(id, cancellationToken);
        if (room == null) return false;

        room.UpdateDetails(
            request.Name,
            request.Description,
            request.Capacity,
            request.Floor,
            request.Type
        );

        _repository.Update(room);
        await _repository.SaveChangesAsync(cancellationToken);

        return true;
    }

    // удалить комнату
    public async Task<bool> DeleteRoomAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var room = await _repository.GetByIdAsync(id, cancellationToken);
        if(room == null) return false;

        room.Deactivate();

        _repository.Update(room);
        await _repository.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static RoomResponse MapToResponse(Room room) =>
        new(
            room.Id,
            room.Name,
            room.Description,
            room.Capacity,
            room.Floor,
            room.Type,
            room.IsActive
        );
}
