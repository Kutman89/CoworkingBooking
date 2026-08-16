using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Booking;

internal class UpdateBookingRequest
{
    [Required]
    public Guid RoomId { get; init; }

    [Required]
    public Guid UserId { get; init; }

    [Required]
    public DateTime StartTime { get; init; }

    [Required]
    public DateTime EndTime { get; init; }

    [Required, EnumDataType(typeof(BookingStatus))]
    public BookingStatus Status { get; init; }
}
