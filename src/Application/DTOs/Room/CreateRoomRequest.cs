using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.Room;

public sealed class CreateRoomRequest
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    public string Description { get; set; } = null!;

    [Range(1, 1000)]
    public int Capacity { get; set; }
    
    [Range(0, 200)]
    public int Floor { get; set; }

    [EnumDataType(typeof(RoomType))]
    public RoomType Type { get; set; }
}
