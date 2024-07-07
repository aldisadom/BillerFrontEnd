using Clients.Interfaces;
using Contracts.Requests.Seller;
using Contracts.Responses;
using Contracts.Responses.Seller;
using Domain.IOptions;
using Microsoft.Extensions.Options;

namespace Clients.Clients;

public class SellerClient : ISellerClient
{
    private readonly BaseHttpClient _userHttpClient;
    private readonly string _controller = "sellers";

    public SellerClient(IOptions<ClientsOptions> clientOptions, IHttpClientFactory httpClientFactory)
    {
        string billioUrl = clientOptions.Value.BillioUrl
            ?? throw new ArgumentNullException($"Billio URL is missing");

        _userHttpClient = new(httpClientFactory, billioUrl);
    }

    public async Task<SellerListResponse> Get()
    {
        return await _userHttpClient.GetAsync<SellerListResponse>($"{_controller}");
    }

    public async Task<SellerListResponse> Get(SellerGetRequest request)
    {
        Dictionary<string, string> headers = new()
        {
            { "UserId", request.UserId.ToString()! }
        };
        return await _userHttpClient.GetAsync<SellerListResponse>($"{_controller}", headers);
    }

    public async Task<SellerResponse?> Get(Guid id)
    {
        return await _userHttpClient.GetAsync<SellerResponse>($"{_controller}/{id}");
    }

    public async Task<AddResponse> Add(SellerAddRequest seller)
    {
        return await _userHttpClient.PostAsync<SellerAddRequest, AddResponse>($"{_controller}", seller);
    }

    public async Task Update(SellerUpdateRequest seller)
    {
        await _userHttpClient.PutAsync($"{_controller}", seller);
    }

    public async Task Delete(Guid id)
    {
        await _userHttpClient.DeleteAsync($"{_controller}/{id}");
    }
}
