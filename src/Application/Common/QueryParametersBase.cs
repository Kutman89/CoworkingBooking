using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common;

public abstract class QueryParametersBase
{
    public string? SortBy { get; init; }
    public bool SortDescending { get; init; }

    private int _page = 1;
    public int Page { get => _page; init => _page = value < 1 ? 1 : value; }

    private int _pageSize = 20;
    public int PageSize { get => _pageSize; init => _pageSize = value is < 1 or > 100 ? 20 : value; }
}
