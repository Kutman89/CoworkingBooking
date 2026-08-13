using Domain.Entities;
using Application.Interfaces;
using Application.DTOs.User;

namespace Application.Services;

public sealed class UserService(IUserRepository repository) : IUserService
{
    // создать пользователя
    public async Task<UserResponse> CreateAsync(
        CreateUserRequest request,
        CancellationToken ct = default)
    {
        var user = new User(
            request.FirstName,
            request.LastName,
            request.Email
        );

        await repository.AddAsync(user, ct);
        await repository.SaveChangesAsync();

        return MapToResponse(user);
    }



    // получить пользователя по айди
    public async Task<UserResponse?> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        var user = await repository.GetByIdAsync(id, ct);
        return user == null ? null : MapToResponse(user);
    }



    // получить всех пользователей
    public async Task<IReadOnlyList<UserResponse>> ListAsync(
        CancellationToken ct = default)
    {
        var users = await repository.GetAllAsync(ct);
        return users.Select(MapToResponse).ToList();
    }



    // заблокировать пользователя
    public async Task<bool> BlockAsync(
        Guid id,
        CancellationToken ct = default)
    {
        var user = await repository.GetByIdAsync(id, ct);
        if (user is null) return false;

        user.Block();

        await repository.Update(user);
        await repository.SaveChangesAsync();

        return true;
    }


    // разблокировать пользователя
    public async Task<bool> UnblockAsync(
        Guid id, CancellationToken ct = default)
    {
        var user = await repository.GetByIdAsync(id, ct);
        if (user is null) return false;

        user.Unblock();

        await repository.Update(user);
        await repository.SaveChangesAsync(ct);

        return true;
    }


    private static UserResponse MapToResponse(User user) => 
        new UserResponse(
        user.Id,
        user.FirstName,
        user.LastName,
        user.Email
    );
}
