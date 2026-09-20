using Domain.Enums;
using Application.Common;

namespace Application.DTOs.Room;

public sealed class RoomQueryParameters : QueryParametersBase
{
    public int? MinCapacity { get; init; }
    public RoomType? Type { get; init; }
}
