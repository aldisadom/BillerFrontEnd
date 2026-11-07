using BillerContracts.Enums;
using Microsoft.AspNetCore.Components.QuickGrid;

namespace FrontEnd.Capabilities;

/// <summary>
/// Page and page size of one server-paged QuickGrid, kept in the URL as <see cref="PageKey"/> and
/// <see cref="PageSizeKey"/> by <see cref="PagedGridsUrl"/>.
/// </summary>
public sealed class GridPaging
{
    public static readonly PageSize[] PageSizes = [PageSize.p10, PageSize.p25, PageSize.p50];

    // QuickGrid works out CurrentPageIndex * ItemsPerPage as an int, so a page index above this
    // overflows at the largest page size before the paginator moves to the last page
    private const int MaxPage = int.MaxValue / 50;

    public GridPaging(string pageKey = "page", string pageSizeKey = "pageSize")
    {
        PageKey = pageKey;
        PageSizeKey = pageSizeKey;
    }

    public string PageKey { get; }
    public string PageSizeKey { get; }
    public PaginationState Pagination { get; } = new() { ItemsPerPage = (int)PageSize.p10 };
    public PageSize PageSize { get; set; } = PageSize.p10;
    public int Page => Pagination.CurrentPageIndex + 1;

    /// <summary>
    /// Moves the grid to the page and page size from the URL. A missing or unknown page size is 10,
    /// a missing page or one below 1 is 1. Biller answers a page past the last one with the last page, and
    /// the paginator then moves there.
    /// </summary>
    public async Task Apply(int? page, int? pageSize)
    {
        PageSize size = PageSizes.FirstOrDefault(s => (int)s == pageSize, PageSize.p10);
        int index = Math.Clamp(page ?? 1, 1, MaxPage) - 1;

        if (size == PageSize && index == Pagination.CurrentPageIndex)
            return;

        PageSize = size;
        Pagination.ItemsPerPage = (int)size;
        await Pagination.SetCurrentPageIndexAsync(index);
    }

    public async Task PageSizeChanged()
    {
        Pagination.ItemsPerPage = (int)PageSize;
        await Pagination.SetCurrentPageIndexAsync(0);
    }
}
