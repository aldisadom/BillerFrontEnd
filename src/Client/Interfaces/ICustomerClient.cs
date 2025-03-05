using Common;
using Contracts.Requests.Customer;
using Contracts.Responses;
using Contracts.Responses.Customer;

namespace Clients.Interfaces;

public interface ICustomerClient
{
    Task<Result<AddResponse>> Add(CustomerAddRequest customer);
    Task<Result<bool>> Delete(Guid id);
    Task<Result<CustomerListResponse>> Get();
    Task<Result<CustomerListResponse>> Get(CustomerGetRequest request);
    Task<Result<CustomerResponse?>> Get(Guid id);
    Task<Result<bool>> Update(CustomerUpdateRequest customer);
}
