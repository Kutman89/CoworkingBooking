using Domain.Enums;

namespace Domain.Entities;

public class Booking
{
    public Guid id { get; set; }
    public Guid roomId { get; set; }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public BookingStatus Status { get; set; }
}
