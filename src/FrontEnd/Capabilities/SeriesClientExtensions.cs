using BillerContracts.Clients.Interfaces;
using BillerContracts.Requests.Series;
using BillerContracts.Responses.Series;

namespace FrontEnd.Capabilities;

internal static class SeriesClientExtensions
{
    public static Task<List<SeriesResponse>> GetAllForSeller(this ISeriesClient client, Guid sellerId) =>
        PagedClientExtensions.GetAllPages(
            (page, pageSize) => client.Get(new SeriesGetRequest { SellerId = sellerId, Page = page, PageSize = pageSize }),
            r => r.Series, s => s.Id);
}
