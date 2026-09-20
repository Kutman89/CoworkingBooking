using Domain.Enums;
using Application.Common;

namespace Application.DTOs.Booking;

public sealed class BookingQueryParameters : QueryParametersBase
{
    public Guid? RoomId { get; init; }
    public Guid? UserId { get; init; }
    public BookingStatus? Status { get; init; }
}
