using Clients.Interfaces;
using Contracts.Requests.InvoiceAddress;
using Contracts.Responses;
using Contracts.Responses.InvoiceAddress;
using Domain.IOptions;
using Microsoft.Extensions.Options;

namespace Clients.Clients;

public class InvoiceAddressClient : IInvoiceAddressClient
{
    private readonly BaseHttpClient _userHttpClient;

    public InvoiceAddressClient(IOptions<ClientsOptions> clientOptions, IHttpClientFactory httpClientFactory)
    {
        string userUrl = clientOptions.Value.UserUrl
            ?? throw new ArgumentNullException($"InvoiceAddresss client URL is missing");

        _userHttpClient = new(httpClientFactory, userUrl);
    }

    public async Task<InvoiceAddressListResponse> Get()
    {
        return await _userHttpClient.GetAsync<InvoiceAddressListResponse>("InvoiceAddress");
    }

    public async Task<InvoiceAddressListResponse> Get(InvoiceAddressGetRequest request)
    {
        Dictionary<string, string> headers = new()
        {
            { "UserId", request.UserId.ToString()! }
        };
        return await _userHttpClient.GetAsync<InvoiceAddressListResponse>("InvoiceAddress", headers);
    }

    public async Task<InvoiceAddressResponse?> Get(Guid id)
    {
        return await _userHttpClient.GetAsync<InvoiceAddressResponse>($"InvoiceAddress/{id}");
    }

    public async Task<AddResponse> Add(InvoiceAddressAddRequest user)
    {
        return await _userHttpClient.PostAsync<InvoiceAddressAddRequest, AddResponse>($"InvoiceAddress", user);
    }

    public async Task Update(InvoiceAddressUpdateRequest user)
    {
        await _userHttpClient.PutAsync($"InvoiceAddress", user);
    }

    public async Task Delete(Guid id)
    {
        await _userHttpClient.DeleteAsync($"InvoiceAddress/{id}");
    }
}
