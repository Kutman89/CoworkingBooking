using Application.Common;

namespace Application.DTOs.User;

public sealed class UserQueryParameters : QueryParametersBase
{
    public bool? IsBlocked { get; init; }
}
