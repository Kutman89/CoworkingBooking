using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs.Booking;

public sealed class CreateBookingRequest
{
    [Required]
    public Guid RoomId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required, EnumDataType(typeof(BookingStatus))]
    public BookingStatus Status { get; set; }
}
