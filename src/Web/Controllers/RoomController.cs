using Application.DTOs.Room;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Application.Services;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;


        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // POST: api/Room 
        [HttpPost]
        public async Task<IActionResult> CreateRoom(
            [FromBody] CreateRoomRequest request)
        {
            await _roomService.CreateRoomAsync(request);

            return Ok();
        }


        // Список всех комнат
        [HttpGet]
        public async Task<IActionResult> GetRooms()
        {
            var rooms = await _roomService.ListAsync();
            return Ok(rooms);
        }


        // комната по айди
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoom(Guid id)
        {
            try 
            {
                var room = await _roomService.GetRoomByIdAsync(id);
                return Ok(room);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // изменение комнаты по айди
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRoom(Guid id, [FromBody] CreateRoomRequest request)
        {
            try 
            {
                await _roomService.UpdateRoomAsync(id, request);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // удаление комнаты по айди
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(Guid id)
        {
            try
            {
                await _roomService.DeleteRoomAsync(id);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}