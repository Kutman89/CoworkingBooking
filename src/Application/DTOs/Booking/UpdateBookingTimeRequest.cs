using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Booking;

public class UpdateBookingTimeRequest
{
    [Required]
    public DateTime StartTime { get; init; }

    [Required]
    public DateTime EndTime { get; init; }
}
