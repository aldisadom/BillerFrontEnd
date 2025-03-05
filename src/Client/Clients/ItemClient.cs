using Clients.Interfaces;
using Common;
using Contracts.Requests.Item;
using Contracts.Responses;
using Contracts.Responses.Item;

namespace Clients.Clients;

public class ItemClient : IItemClient
{
    private readonly BaseHttpClient _userHttpClient;
    private readonly string _controller = "items";

    public ItemClient()
    {
        string billioUrl = "https://localhost:8091";

        _userHttpClient = new(billioUrl);
    }

    public async Task<Result<ItemListResponse>> Get()
    {
        return await _userHttpClient.GetAsync<ItemListResponse>($"{_controller}");
    }

    public async Task<Result<ItemListResponse>> Get(ItemGetRequest request)
    {
        Dictionary<string, string> headers = new()
        {
            { "CustomerId", request.CustomerId.ToString()! }
        };
        return await _userHttpClient.GetAsync<ItemListResponse>($"{_controller}", headers);
    }

    public async Task<Result<ItemResponse?>> Get(Guid id)
    {
        return await _userHttpClient.GetAsync<ItemResponse?>($"{_controller}/{id}");
    }

    public async Task<Result<AddResponse>> Add(ItemAddRequest item)
    {
        return await _userHttpClient.PostAsync<ItemAddRequest, AddResponse>($"{_controller}", item);
    }

    public async Task<Result<bool>> Update(ItemUpdateRequest item)
    {
        return await _userHttpClient.PutAsync($"{_controller}", item);
    }

    public async Task<Result<bool>> Delete(Guid id)
    {
        return await _userHttpClient.DeleteAsync($"{_controller}/{id}");
    }
}
