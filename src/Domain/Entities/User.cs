using Domain.Exceptions;

namespace Domain.Entities;
public class User
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public bool IsBlocked { get; private set; }

    private User() { }

    public User(string firstName, string lastName, string email, string password)
    {
        Id = Guid.NewGuid();
        UpdateProfile(firstName, lastName, email);
        SetPasswordHash(password);
        IsBlocked = false;
    }

    public void SetPasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new DomainExceptions("Хеш пароля не может быть пустым", nameof(passwordHash));

        PasswordHash = passwordHash;
    }

    public void UpdateProfile(string firstName, string lastName, string email)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainExceptions("Имя не может быть пустым", nameof(firstName));
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainExceptions("Фамилия не может быть пустой", nameof(lastName));
        
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new DomainExceptions("Email не может быть пустым", nameof(email));
        
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public void Block(){
        if (IsBlocked)
        {
            throw new DomainExceptions("Пользователь уже заблокирован", nameof(IsBlocked));
        }

        IsBlocked = true;
    }
    public void Unblock(){
        if (!IsBlocked)
        {
            throw new DomainExceptions("Пользователь уже разблокирован", nameof(IsBlocked));
        }
        IsBlocked = false;
    }
}
