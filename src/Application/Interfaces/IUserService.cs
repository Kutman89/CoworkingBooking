using Application.DTOs.User;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserResponse> CreateAsync(CreateUserRequest request, CancellationToken ct = default);
    Task<UserResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<UserResponse>> ListAsync(CancellationToken ct = default);
    Task<bool> BlockAsync(Guid id, CancellationToken ct = default);
    Task<bool> UnblockAsync(Guid id, CancellationToken ct = default);
}
