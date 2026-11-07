using BillerContracts.Clients.Interfaces;
using BillerContracts.Enums;
using BillerContracts.Requests.Customer;
using BillerContracts.Responses.Customer;

namespace FrontEnd.Capabilities;

internal static class CustomerClientExtensions
{
    public static async Task<List<CustomerResponse>> GetAllForSeller(this ICustomerClient client, Guid sellerId)
    {
        List<CustomerResponse> customers = [];
        int page = 1;
        CustomerListResponse response;
        do
        {
            response = await client.Get(new CustomerGetRequest { SellerId = sellerId, Page = page, PageSize = PageSize.p50 });

            // A page past the last one comes back as the last page, whose rows are already in the list
            if (response.Page != page++)
                break;

            customers.AddRange(response.Customers);
        }
        while (response.Customers.Count > 0 && customers.Count < response.TotalCount);

        return customers;
    }
}
