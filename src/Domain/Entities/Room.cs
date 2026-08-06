using Domain.Enums;

namespace Domain.Entities;

public class Room
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public int Capacity { get; private set; }
    public int Floor { get; private set; }
    public RoomType Type { get; private set; }
    public bool IsActive { get; private set; }


    private Room() { }

    // Конструктор создания новой комнаты
    public Room(string name, string description, int capacity, int floor, RoomType type)
    {
        Id = Guid.NewGuid();
        UpdateDetails(name, description, capacity, floor, type);
        IsActive = true;
    }

    // обновления данных комнаты
    public void UpdateDetails(string name, string description, int capacity, int floor, RoomType type)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Имя комнаты не может быть пустым", nameof(name));

        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Вместимость должна быть больше 0");
        
        if(!Enum.IsDefined(type))
            throw new ArgumentOutOfRangeException(nameof(type), "Недопустимый тип комнаты");
        
        Name = name;
        Description = description;
        Capacity = capacity;
        Floor = floor;
        Type = type;
    }

    public void Deactivate() => IsActive = false;

    public void Activate() => IsActive = true;
}