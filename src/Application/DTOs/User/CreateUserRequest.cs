using System.ComponentModel.DataAnnotations;
namespace Application.DTOs.User;

public sealed class CreateUserRequest
{
    [Required, StringLength(50, MinimumLength = 2)]
    public string FirstName { get; init; } = string.Empty;


    [Required, StringLength(50, MinimumLength = 2)]
    public string LastName { get; init; } = string.Empty;
    
    
    [Required, EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required, StringLength(100, MinimumLength = 8)]
    public string PasswordHash { get; init; } = string.Empty;
}
