using Contracts.Requests.Customer;
using Contracts.Responses;
using Contracts.Responses.Customer;

namespace Clients.Interfaces;

public interface ICustomerClient
{
    Task<AddResponse> Add(CustomerAddRequest customer);
    Task Delete(Guid id);
    Task<CustomerListResponse> Get();
    Task<CustomerListResponse> Get(CustomerGetRequest request);
    Task<CustomerResponse?> Get(Guid id);
    Task Update(CustomerUpdateRequest customer);
}