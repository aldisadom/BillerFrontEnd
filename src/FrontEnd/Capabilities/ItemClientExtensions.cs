using BillerContracts.Clients.Interfaces;
using BillerContracts.Requests.Item;
using BillerContracts.Responses.Item;

namespace FrontEnd.Capabilities;

internal static class ItemClientExtensions
{
    public static Task<List<ItemResponse>> GetAllForCustomer(this IItemClient client, Guid customerId) =>
        PagedClientExtensions.GetAllPages(
            (page, pageSize) => client.Get(new ItemGetRequest { CustomerId = customerId, Page = page, PageSize = pageSize }),
            r => r.Items, i => i.Id);
}
