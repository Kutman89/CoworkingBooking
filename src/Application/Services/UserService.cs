using Application.Common;
using Application.DTOs.Booking;
using Application.DTOs.User;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public sealed class UserService(IUserRepository repository, IPasswordHasher passwordHasher) : IUserService
{
    // создать пользователя
    public async Task<UserResponse> CreateAsync(
        CreateUserRequest request,
        CancellationToken ct = default)
    {
        var passwordHash = passwordHasher.Hash(request.Password);

        var user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            passwordHash
        );

        await repository.AddAsync(user, ct);
        await repository.SaveChangesAsync(ct);

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
    public async Task<PagedResult<UserResponse>> ListAsync(
        UserQueryParameters query,
        CancellationToken ct = default)
    {
        var result = await repository.GetPagedAsync(query, ct);
        return new PagedResult<UserResponse>(
            result.Items.Select(MapToResponse).ToList(),
            result.TotalCount, result.Page, result.PageSize
        );
    }

    // заблокировать пользователя
    public async Task<bool> BlockAsync(
        Guid id,
        CancellationToken ct = default)
    {
        var user = await repository.GetByIdAsync(id, ct);
        if (user is null) return false;

        user.Block();

        repository.Update(user);
        await repository.SaveChangesAsync(ct);

        return true;
    }

    // разблокировать пользователя
    public async Task<bool> UnblockAsync(
        Guid id, CancellationToken ct = default)
    {
        var user = await repository.GetByIdAsync(id, ct);
        if (user is null) return false;

        user.Unblock();

        repository.Update(user);
        await repository.SaveChangesAsync(ct);

        return true;
    }

    private static UserResponse MapToResponse(User user) => 
        new (
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.IsBlocked
        );
}