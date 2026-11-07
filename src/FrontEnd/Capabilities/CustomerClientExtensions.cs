using BillerContracts.Clients.Interfaces;
using BillerContracts.Requests.Customer;
using BillerContracts.Responses.Customer;

namespace FrontEnd.Capabilities;

internal static class CustomerClientExtensions
{
    public static Task<List<CustomerResponse>> GetAllForSeller(this ICustomerClient client, Guid sellerId) =>
        PagedClientExtensions.GetAllPages(
            (page, pageSize) => client.Get(new CustomerGetRequest { SellerId = sellerId, Page = page, PageSize = pageSize }),
            r => r.Customers, c => c.Id);
}
