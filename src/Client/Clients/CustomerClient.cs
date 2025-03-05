using Clients.Interfaces;
using Common;
using Contracts.Requests.Customer;
using Contracts.Responses;
using Contracts.Responses.Customer;

namespace Clients.Clients;

public class CustomerClient : ICustomerClient
{
    private readonly BaseHttpClient _userHttpClient;
    private readonly string _controller = "customers";

    public CustomerClient()
    {
        string billioUrl = "https://localhost:8091";

        _userHttpClient = new(billioUrl);
    }

    public async Task<Result<CustomerListResponse>> Get()
    {
        return await _userHttpClient.GetAsync<CustomerListResponse>($"{_controller}");
    }

    public async Task<Result<CustomerListResponse>> Get(CustomerGetRequest request)
    {
        Dictionary<string, string> headers = new()
        {
            { "SellerId", request.SellerId.ToString()! }
        };
        return await _userHttpClient.GetAsync<CustomerListResponse>($"{_controller}", headers);
    }

    public async Task<Result<CustomerResponse?>> Get(Guid id)
    {
        return await _userHttpClient.GetAsync<CustomerResponse?>($"{_controller}/{id}");
    }

    public async Task<Result<AddResponse>> Add(CustomerAddRequest customer)
    {
        return await _userHttpClient.PostAsync<CustomerAddRequest, AddResponse>($"{_controller}", customer);
    }

    public async Task<Result<bool>> Update(CustomerUpdateRequest customer)
    {
        return await _userHttpClient.PutAsync($"{_controller}", customer);
    }

    public async Task<Result<bool>> Delete(Guid id)
    {
        return await _userHttpClient.DeleteAsync($"{_controller}/{id}");
    }
}
