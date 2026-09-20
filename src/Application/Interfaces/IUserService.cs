using Application.Common;
using Application.DTOs.User;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserResponse> CreateAsync(CreateUserRequest request, CancellationToken ct = default);
    Task<UserResponse?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<PagedResult<UserResponse>> ListAsync(UserQueryParameters query, CancellationToken ct = default);
    Task<bool> BlockAsync(Guid id, CancellationToken ct = default);
    Task<bool> UnblockAsync(Guid id, CancellationToken ct = default);
}
