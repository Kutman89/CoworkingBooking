namespace Domain.Entities;
public class User
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public bool IsBlocked { get; private set; }

    private User() { }

    public User(string firstName, string lastName, string email)
    {
        Id = Guid.NewGuid();
        UpdateProfile(firstName, lastName, email);
        IsBlocked = false;
    }

    public void UpdateProfile(string firstName, string lastName, string email)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("Имя не может быть пустым", nameof(firstName));
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Фамилия не может быть пустой", nameof(lastName));
        
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            throw new ArgumentException("Email не может быть пустым", nameof(email));
        
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public void Block() => IsBlocked = true;
    public void Unblock() => IsBlocked = false;
}
