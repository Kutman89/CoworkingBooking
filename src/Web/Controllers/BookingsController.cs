using Application.DTOs.Booking;
using Application.DTOs.Room;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingsController(IBookingService bookingService) : ControllerBase
{
    // создание бронирования
    [HttpPost]
    [ProducesResponseType<BookingResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookingResponse>> Create(
        [FromBody] CreateBookingRequest request,
        CancellationToken ct)
    {
        var booking = await bookingService.CreateAsync(request, ct);
        return CreatedAtAction(
            nameof(GetById),
            new { id = booking.Id }, booking);
    }

    // список всех броней
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<BookingResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<BookingResponse>>> GetAll(
        CancellationToken ct)
    {
        var bookings = await bookingService.ListAsync(ct);
        return Ok(bookings);
    }

    // бронь по айди
    [HttpGet("{id:guid}")]
    [ProducesResponseType<BookingResponse>(StatusCodes.Status200OK)]
    public async Task<ActionResult<BookingResponse>> GetById(
        Guid id,
        CancellationToken ct)
    {
        var booking = await bookingService.GetByIdAsync(id, ct);
        if(booking is null) return NotFound();

        return Ok(booking);
    }

    // обновление времени
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTimes(
        Guid id,
        UpdateBookingTimeRequest request,
        CancellationToken ct)
    {
        var updated = await bookingService.UpdateTimeAsync(id, request, ct);
        return updated ? Ok() : NotFound();
    }

    // удаления бронирования
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken ct)
    {
        var deleted = await bookingService.DeleteAsync(id, ct);
        return deleted ? Ok() : NotFound();
    }

}
