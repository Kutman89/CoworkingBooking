using Domain.Enums;
using Domain.Exceptions;
using System.Xml.Linq;

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

    public static Room Create(string name, string description, int capacity, int floor, RoomType type)
    {
        InspectionOfParts(name, description, capacity, floor, type);

        return new Room
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Capacity = capacity,
            Floor = floor,
            Type = type,
            IsActive = true
        };
    }

    public void UpdateDetails(string name, string description, int capacity, int floor, RoomType type)
    {
        InspectionOfParts(name, description, capacity, floor, type);

        Name = name;
        Description = description;
        Capacity = capacity;
        Floor = floor;
        Type = type;
    }

    public void Deactivate(){
        if (!IsActive)
        {
            throw new DomainException("Комната уже не активна", nameof(IsActive));
        }

        IsActive = false;
    }

    public void Activate()
    {
        if (IsActive)
        {
            throw new DomainException("Комната уже активна", nameof(IsActive));
        }

        IsActive = true;
    }

    // Проверки
    private static void InspectionOfParts(string name, string description, int capacity, int floor, RoomType type)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Имя комнаты не может быть пустым", nameof(name));

        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Описание комнаты не может быть пустым", nameof(description));

        if (capacity <= 0)
            throw new DomainException("Вместимость должна быть больше 0", nameof(capacity));

        if (floor < 0 || floor > 200)
            throw new DomainException("Недопустимый номер этажа", nameof(floor));

        if (!Enum.IsDefined(type))
            throw new DomainException("Недопустимый тип комнаты", nameof(type));
    }
}