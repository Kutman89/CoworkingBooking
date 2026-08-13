using Application.DTOs.Room;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public sealed class RoomService(IRoomRepository repository) : IRoomService
{
    // создать комнату
    public async Task<RoomResponse> CreateAsync(
        CreateRoomRequest request,
        CancellationToken ct = default)
    {
        var room = new Room(
            request.Name,
            request.Description,
            request.Capacity,
            request.Floor,
            request.Type
        );
        await repository.AddAsync(room, ct);
        await repository.SaveChangesAsync(ct);

        return MapToResponse(room);
    }


    // получить все комнаты
    public async Task<IReadOnlyList<RoomResponse>> ListAsync(
        CancellationToken ct = default)
    {
        var rooms = await repository.GetAllAsync(ct);
        return rooms.Select(MapToResponse).ToArray();
    }



    // получить по айди
    public async Task<RoomResponse?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        var room = await repository.GetByIdAsync(id, ct);
        return room == null ? null : MapToResponse(room);
    }



    // обновить комнату
    public async Task<bool> UpdateAsync(
        Guid id,
        CreateRoomRequest request,
        CancellationToken ct = default)
    {
        var room = await repository.GetByIdAsync(id, ct);
        
        if (room is null) return false;

        room.UpdateDetails(
            request.Name,
            request.Description,
            request.Capacity,
            request.Floor,
            request.Type
        );

        repository.Update(room);
        await repository.SaveChangesAsync(ct);
        return true;
    }



    // удалить комнату
    public async Task<bool> DeleteAsync(
        Guid id,
        CancellationToken ct = default)
    {
        var room = await repository.GetByIdAsync(id, ct);
        if(room is null) return false;

        room.Deactivate();

        repository.Update(room);
        await repository.SaveChangesAsync(ct);
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
