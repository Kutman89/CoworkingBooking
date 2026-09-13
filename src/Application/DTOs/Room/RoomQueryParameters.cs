using Domain.Enums;

namespace Application.DTOs.Room;

public sealed class RoomQueryParameters
{
    public int? MinCapacity { get; init; }
    public RoomType? Type { get; init; }

    public string? SortBy { get; init; } = "name"; // name, capacity, floor
    public bool SortDescending { get; init; } = false;

    private int _page = 1;
    public int Page { get => _page; init => _page = value < 1 ? 1 : value; }

    private int _pageSize = 20;
    public int PageSize { get => _pageSize; init => _pageSize = value is 1 or > 100 ? 20 : value; }
}
