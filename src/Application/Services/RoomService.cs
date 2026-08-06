using Application.DTOs.Room;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public sealed class RoomService(IRoomRepository repository) : IRoomService
{
    // создать комнату
    public async Task<RoomResponse> CreateAsync(
        CreateRoomRequest request,
        CancellationToken cancellationToken = default)
    {
        var room = new Room(
            request.Name,
            request.Description,
            request.Capacity,
            request.Floor,
            request.Type
        );
        await repository.AddAsync(room, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        return MapToResponse(room);
    }

    // получить все комнаты
    public async Task<IReadOnlyList<RoomResponse>> ListAsync(
        CancellationToken cancellationToken = default)
    {
        var rooms = await repository.GetAllAsync(cancellationToken);
        return rooms.Select(MapToResponse).ToArray();
    }

    // получить по айди
    public async Task<RoomResponse?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var room = await repository.GetByIdAsync(id, cancellationToken);
        return room == null ? null : MapToResponse(room);
    }

    // обновить комнату
    public async Task<bool> UpdateAsync(
        Guid id,
        CreateRoomRequest request,
        CancellationToken cancellationToken = default)
    {
        var room = await repository.GetByIdAsync(id, cancellationToken);
        
        if (room is null) return false;

        room.UpdateDetails(
            request.Name,
            request.Description,
            request.Capacity,
            request.Floor,
            request.Type
        );

        repository.Update(room);
        await repository.SaveChangesAsync(cancellationToken);
        return true;
    }

    // удалить комнату
    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var room = await repository.GetByIdAsync(id, cancellationToken);
        if(room is null) return false;

        room.Deactivate();

        repository.Update(room);
        await repository.SaveChangesAsync(cancellationToken);
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
