using Clients.Interfaces;
using Contracts.Requests.Item;
using Contracts.Responses;
using Contracts.Responses.Item;
using Domain.IOptions;
using Microsoft.Extensions.Options;

namespace Clients.Clients;

public class ItemClient : IItemClient
{
    private readonly BaseHttpClient _userHttpClient;
    private readonly string _controller = "items";

    public ItemClient(IOptions<ClientsOptions> clientOptions, IHttpClientFactory httpClientFactory)
    {
        string billioUrl = clientOptions.Value.BillioUrl
            ?? throw new ArgumentNullException($"URL is missing {nameof(clientOptions.Value.BillioUrl)}");

        _userHttpClient = new(httpClientFactory, billioUrl);
    }

    public async Task<ItemListResponse> Get()
    {
        return await _userHttpClient.GetAsync<ItemListResponse>($"{_controller}");
    }

    public async Task<ItemListResponse> Get(ItemGetRequest request)
    {
        Dictionary<string, string> headers = new()
        {
            { "CustomerId", request.CustomerId.ToString()! }
        };
        return await _userHttpClient.GetAsync<ItemListResponse>($"{_controller}", headers);
    }

    public async Task<ItemResponse?> Get(Guid id)
    {
        return await _userHttpClient.GetAsync<ItemResponse>($"{_controller}/{id}");
    }

    public async Task<AddResponse> Add(ItemAddRequest item)
    {
        return await _userHttpClient.PostAsync<ItemAddRequest, AddResponse>($"{_controller}", item);
    }

    public async Task Update(ItemUpdateRequest item)
    {
        await _userHttpClient.PutAsync($"{_controller}", item);
    }

    public async Task Delete(Guid id)
    {
        await _userHttpClient.DeleteAsync($"{_controller}/{id}");
    }
}
