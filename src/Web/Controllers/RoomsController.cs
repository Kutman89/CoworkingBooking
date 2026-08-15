using Application.DTOs.Room;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomsController(IRoomService roomService) : ControllerBase
{
    // POST: api/Room 
    [HttpPost]
    [ProducesResponseType<RoomResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<RoomResponse>> Create(
        [FromBody] CreateRoomRequest request,
        CancellationToken ct)
    {
        var room = await roomService.CreateAsync(request, ct);
        return CreatedAtAction(
            nameof(GetById),
            new { id = room.Id}, room);
    }


    // Список всех комнат
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<RoomResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<RoomResponse>>> GetAll(
        CancellationToken ct)
    {
        var rooms = await roomService.ListAsync(ct);
        return Ok(rooms);
    }


    // комната по айди
    [HttpGet("{id:guid}")]
    [ProducesResponseType<RoomResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RoomResponse>> GetById(
        Guid id,
        CancellationToken ct)
    {
        var room = await roomService.GetByIdAsync(id, ct);
        if (room == null) return NotFound();

        return Ok(room);            
    }

    // изменение комнаты по айди
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateRoomRequest request,
        CancellationToken ct)
    {
        var updated = await roomService.UpdateAsync(id, request, ct);
        
        return updated ? NoContent() : NotFound();
    }

    // удаление комнаты по айди
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken ct)
    {
        var deleted = await roomService.DeleteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}