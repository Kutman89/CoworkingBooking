using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities;

public class Booking
{
    public Guid Id { get; private set; }
    public Guid RoomId { get; private set; }
    public Guid UserId { get; private set; }

    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    public BookingStatus Status { get; private set; }

    private Booking() { }

    public static Booking Create(Guid roomId, Guid userId, DateTime startTime, DateTime endTime)
    {
        if(roomId == Guid.Empty)
        {
            throw new DomainException("Недопустимый идентификатор комнаты", nameof(roomId));
        }
        if (userId == Guid.Empty) 
        {
            throw new DomainException("Недопустимый идентификатор пользователя", nameof(userId));
        }

        if(startTime.Kind != DateTimeKind.Utc || endTime.Kind != DateTimeKind.Utc)
            throw new DomainException("Время бронирования должно указываться в формате UTC", nameof(startTime));

        ValidateTimes(startTime, endTime);


        return new Booking
        {
            Id = Guid.NewGuid(),
            RoomId = roomId,
            UserId = userId,
            StartTime = startTime,
            EndTime = endTime,
            Status = BookingStatus.Pending
        };
    }

    public void Confirm()
    {
        if (Status != BookingStatus.Pending)
            throw new DomainException("Подтвердить можно только бронирование в статусе Pending", nameof(Status));

        Status = BookingStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status is BookingStatus.Completed or BookingStatus.Cancelled)
            throw new DomainException("Нельзя отменить завершённое или уже отменённое бронирование", nameof(Status));

        Status = BookingStatus.Cancelled;
    }

    public void Complete()
    {
        if (Status != BookingStatus.Confirmed)
            throw new DomainException("Завершить можно только подтверждённое бронирование", nameof(Status));

        Status = BookingStatus.Completed;
    }

    public void ChangeRoom(Guid newRoomId)
    {
        EnsureModifiable();

        if (newRoomId == Guid.Empty)
            throw new DomainException("Недопустимый идентификатор комнаты", nameof(newRoomId));

        RoomId = newRoomId;
    }

    public void UpdateBookingTimes(DateTime newStartTime, DateTime newEndTime)
    {
        EnsureModifiable();

        ValidateTimes(newStartTime, newEndTime);

        StartTime = newStartTime;
        EndTime = newEndTime;
    }

    private void EnsureModifiable()
    {
        if (Status is BookingStatus.Completed or BookingStatus.Cancelled)
            throw new DomainException("Нельзя изменить завершённое или уже отменённое бронирование", nameof(Status));
    }

    private static void ValidateTimes(DateTime start, DateTime end) 
    {
        if (start >= end) 
        {
            throw new DomainException("Начало должно быть до окончания", nameof (start));
        }
    }
}
