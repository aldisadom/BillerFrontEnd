using Clients.Interfaces;
using Contracts.Requests.InvoiceItem;
using Contracts.Responses;
using Contracts.Responses.InvoiceItem;
using Domain.IOptions;
using Microsoft.Extensions.Options;

namespace Clients.Clients;

public class InvoiceItemClient : IInvoiceItemClient
{
    private readonly BaseHttpClient _userHttpClient;

    public InvoiceItemClient(IOptions<ClientsOptions> clientOptions, IHttpClientFactory httpClientFactory)
    {
        string userUrl = clientOptions.Value.UserUrl
            ?? throw new ArgumentNullException($"InvoiceItem client URL is missing");

        _userHttpClient = new(httpClientFactory, userUrl);
    }

    public async Task<InvoiceItemListResponse> Get()
    {
        return await _userHttpClient.GetAsync<InvoiceItemListResponse>("InvoiceItem");
    }

    public async Task<InvoiceItemListResponse> Get(InvoiceItemGetRequest request)
    {
        Dictionary<string, string> headers = new()
        {
            { "AddressId", request.AddressId.ToString()! }
        };
        return await _userHttpClient.GetAsync<InvoiceItemListResponse>("InvoiceItem", headers);
    }

    public async Task<InvoiceItemResponse?> Get(Guid id)
    {
        return await _userHttpClient.GetAsync<InvoiceItemResponse>($"InvoiceItem/{id}");
    }

    public async Task<AddResponse> Add(InvoiceItemAddRequest user)
    {
        return await _userHttpClient.PostAsync<InvoiceItemAddRequest, AddResponse>($"InvoiceItem", user);
    }

    public async Task Update(InvoiceItemUpdateRequest user)
    {
        await _userHttpClient.PutAsync($"InvoiceItem", user);
    }

    public async Task Delete(Guid id)
    {
        await _userHttpClient.DeleteAsync($"InvoiceItem/{id}");
    }
}
