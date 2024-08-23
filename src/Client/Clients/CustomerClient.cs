using Clients.Interfaces;
using Contracts.Requests.Customer;
using Contracts.Responses;
using Contracts.Responses.Customer;
using Domain.IOptions;
using Microsoft.Extensions.Options;

namespace Clients.Clients;

public class CustomerClient : ICustomerClient
{
    private readonly BaseHttpClient _userHttpClient;
    private readonly string _controller = "customers";

    public CustomerClient(IOptions<ClientsOptions> clientOptions, IHttpClientFactory httpClientFactory)
    {
        string billioUrl = clientOptions.Value.BillioUrl
            ?? throw new ArgumentNullException($"URL is missing {nameof(clientOptions.Value.BillioUrl)}");

        _userHttpClient = new(httpClientFactory, billioUrl);
    }

    public async Task<CustomerListResponse> Get()
    {
        return await _userHttpClient.GetAsync<CustomerListResponse>($"{_controller}");
    }

    public async Task<CustomerListResponse> Get(CustomerGetRequest request)
    {
        Dictionary<string, string> headers = new()
        {
            { "SellerId", request.SellerId.ToString()! }
        };
        return await _userHttpClient.GetAsync<CustomerListResponse>($"{_controller}", headers);
    }

    public async Task<CustomerResponse?> Get(Guid id)
    {
        return await _userHttpClient.GetAsync<CustomerResponse>($"{_controller}/{id}");
    }

    public async Task<AddResponse> Add(CustomerAddRequest customer)
    {
        return await _userHttpClient.PostAsync<CustomerAddRequest, AddResponse>($"{_controller}", customer);
    }

    public async Task Update(CustomerUpdateRequest customer)
    {
        await _userHttpClient.PutAsync($"{_controller}", customer);
    }

    public async Task Delete(Guid id)
    {
        await _userHttpClient.DeleteAsync($"{_controller}/{id}");
    }
}
