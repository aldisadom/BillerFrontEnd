using BillerContracts.Clients.Interfaces;
using BillerContracts.Requests.Seller;
using BillerContracts.Responses.Seller;

namespace FrontEnd.Capabilities;

internal static class SellerClientExtensions
{
    public static Task<List<SellerResponse>> GetAllForUser(this ISellerClient client, Guid userId) =>
        PagedClientExtensions.GetAllPages(
            (page, pageSize) => client.Get(new SellerGetRequest { UserId = userId, Page = page, PageSize = pageSize }),
            r => r.Sellers, s => s.Id);
}
