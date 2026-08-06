using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.Room;

public sealed class UpdateRoomRequest
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; init; } = string.Empty;

    [StringLength(500)]
    public string Description { get; init; } = string.Empty;

    [Range(1, 1000)]
    public int Capacity { get; init; }

    [Range(0, 200)]
    public int Floor { get; init; }

    [EnumDataType(typeof(RoomType))]
    public RoomType Type { get; init; }
}
