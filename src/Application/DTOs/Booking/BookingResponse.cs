using Domain.Enums;

namespace Application.DTOs.Booking;

public record BookingResponse(
    Guid Id,
    Guid RoomId,
    Guid UserId,
    DateTime StartTime,
    DateTime EndTime,
    BookingStatus Status);
