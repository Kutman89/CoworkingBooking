using Application.DTOs.Room;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _roomService;
    public RoomsController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    // POST: api/Room 
    [HttpPost]
    public async Task<IActionResult> CreateRoom(
        [FromBody] CreateRoomRequest request,
        CancellationToken cancellationToken)
    {
        var id = await _roomService.CreateRoomAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetRoom), new { id }, null);
    }


    // Список всех комнат
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomResponse>>> GetRooms(CancellationToken cancellationToken)
    {
        var rooms = await _roomService.ListAsync(cancellationToken);
        return Ok(rooms);
    }


    // комната по айди
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RoomResponse>> GetRoom(Guid id, CancellationToken cancellationToken)
    {
        var room = await _roomService.GetRoomByIdAsync(id, cancellationToken);
        if (room == null) return NotFound();

        return Ok(room);            
    }

    // изменение комнаты по айди
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateRoom(Guid id, [FromBody] CreateRoomRequest request, CancellationToken cancellationToken)
    {
        var updated = await _roomService.UpdateRoomAsync(id, request, cancellationToken);
        if(!updated) return NotFound();

        return NoContent();        
    }

    // удаление комнаты по айди
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRoom(Guid id, CancellationToken cancellationToken)
    {
            
        var deleted = await _roomService.DeleteRoomAsync(id, cancellationToken);
        if(!deleted) return NotFound();

        return NoContent();            
    }
}