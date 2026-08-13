namespace Application.DTOs.User;

public record UserResponse
(
    Guid Id,
    string FirstName,
    string LastName,
    string Email
);