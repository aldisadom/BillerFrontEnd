using BillerContracts.Clients.Interfaces;
using BillerContracts.Enums;
using BillerContracts.Requests.Seller;
using BillerContracts.Responses.Seller;

namespace FrontEnd.Capabilities;

internal static class SellerClientExtensions
{
    public static async Task<List<SellerResponse>> GetAllForUser(this ISellerClient client, Guid userId)
    {
        List<SellerResponse> sellers = [];
        int page = 1;
        SellerListResponse response;
        do
        {
            response = await client.Get(new SellerGetRequest { UserId = userId, Page = page++, PageSize = PageSize.p50 });
            sellers.AddRange(response.Sellers);
        }
        while (response.Sellers.Count > 0 && sellers.Count < response.TotalCount);

        return sellers;
    }
}
