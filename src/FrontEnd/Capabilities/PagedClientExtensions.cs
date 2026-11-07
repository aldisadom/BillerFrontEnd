using BillerContracts.Enums;
using BillerContracts.Responses;

namespace FrontEnd.Capabilities;

internal static class PagedClientExtensions
{
    /// <summary>
    /// Loads every page at the largest page size. Biller pages by position in a sorted list, so a row
    /// added or renamed meanwhile can come back on two pages: each id is kept once.
    /// </summary>
    public static async Task<List<TRow>> GetAllPages<TResponse, TRow>(
        Func<int, PageSize, Task<TResponse>> getPage, Func<TResponse, List<TRow>> rowsOf, Func<TRow, Guid> idOf)
        where TResponse : PagedResponse
    {
        List<TRow> rows = [];
        HashSet<Guid> ids = [];
        int page = 1;
        TResponse response;
        do
        {
            response = await getPage(page, PageSize.p50);

            // A page past the last one comes back as the last page, whose rows are already in the list
            if (response.Page != page++)
                break;

            rows.AddRange(rowsOf(response).Where(r => ids.Add(idOf(r))));
        }
        while (rowsOf(response).Count > 0 && rows.Count < response.TotalCount);

        return rows;
    }
}
