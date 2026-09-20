using Application.Common;
using Domain.Entities;
using Application.DTOs.User;

namespace Application.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken ct = default);
    Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<PagedResult<User>> GetPagedAsync(UserQueryParameters query, CancellationToken ct = default);
    void Update(User user);
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
