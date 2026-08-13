using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs.User;

namespace Web.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUserService userService) : ControllerBase
{
    // создать пользователя
    [HttpPost]
    [ProducesResponseType<UserResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserResponse>> Create(
        [FromBody] CreateUserRequest request,
        CancellationToken ct)
    {
        var user = await userService.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }


    // получить пользователя по айди
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponse>> GetById(
        Guid id,
        CancellationToken ct)
    {
        var user = await userService.GetByIdAsync(id, ct);
        if (user == null) return NotFound();
        return Ok(user);
    }


    // получить всех пользователей
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<UserResponse>>> GetAll(
        CancellationToken ct)
    {
        var users = await userService.ListAsync(ct);
        return Ok(users);
    }


    // блокировка пользователя
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Block(
        Guid id,
        CancellationToken ct)
    {
        var userBlock = await userService.BlockAsync(id, ct);
        return userBlock ? Ok() : NotFound();
    }


    // разблокировка пользователя
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Unblock(
        Guid id,
        CancellationToken ct)
    {
        var userUnblock = await userService.UnblockAsync(id, ct);
        return userUnblock ? Ok() : NotFound();
    }
}
