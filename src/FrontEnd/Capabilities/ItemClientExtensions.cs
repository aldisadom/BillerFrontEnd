using BillerContracts.Clients.Interfaces;
using BillerContracts.Enums;
using BillerContracts.Requests.Item;
using BillerContracts.Responses.Item;

namespace FrontEnd.Capabilities;

internal static class ItemClientExtensions
{
    public static async Task<List<ItemResponse>> GetAllForCustomer(this IItemClient client, Guid customerId)
    {
        List<ItemResponse> items = [];
        int page = 1;
        ItemListResponse response;
        do
        {
            response = await client.Get(new ItemGetRequest { CustomerId = customerId, Page = page, PageSize = PageSize.p50 });

            // A page past the last one comes back as the last page, whose rows are already in the list
            if (response.Page != page++)
                break;

            items.AddRange(response.Items);
        }
        while (response.Items.Count > 0 && items.Count < response.TotalCount);

        return items;
    }
}
