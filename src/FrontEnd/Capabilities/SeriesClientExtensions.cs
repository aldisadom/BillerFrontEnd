using BillerContracts.Clients.Interfaces;
using BillerContracts.Enums;
using BillerContracts.Requests.Series;
using BillerContracts.Responses.Series;

namespace FrontEnd.Capabilities;

internal static class SeriesClientExtensions
{
    public static async Task<List<SeriesResponse>> GetAllForSeller(this ISeriesClient client, Guid sellerId)
    {
        List<SeriesResponse> series = [];
        int page = 1;
        SeriesListResponse response;
        do
        {
            response = await client.Get(new SeriesGetRequest { SellerId = sellerId, Page = page++, PageSize = PageSize.p50 });
            series.AddRange(response.Series);
        }
        while (response.Series.Count > 0 && series.Count < response.TotalCount);

        return series;
    }
}
