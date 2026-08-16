using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.Room;

public sealed class CreateRoomRequest
{
    [Required, StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Required, Range(1, 1000)]
    public int Capacity { get; set; }
    
    [Required, Range(0, 200)]
    public int Floor { get; set; }

    [Required, EnumDataType(typeof(RoomType))]
    public RoomType Type { get; set; }
}
