using Domain.Enums;

namespace Application.DTOs.Room;

public record RoomResponse(Guid Id, string Name, string Description, int Capacity, int Floor, RoomType Type, bool IsActive);
