using Application.DTOs.Room;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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


        [HttpPost]
        public async Task<IActionResult> CreateRoom(
            [FromBody] CreateRoomRequest request)
        {
            await _roomService.CreateAsync(request);

            return Ok();
        }
    }
}