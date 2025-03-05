using Clients.Interfaces;
using Common;
using Contracts.Requests.Seller;
using Contracts.Responses;
using Contracts.Responses.Seller;

namespace Clients.Clients;

public class SellerClient : ISellerClient
{
    private readonly BaseHttpClient _userHttpClient;
    private readonly string _controller = "sellers";

    public SellerClient()
    {
        string billioUrl = "https://localhost:8091";

        _userHttpClient = new(billioUrl);
    }

    public async Task<Result<SellerListResponse>> Get()
    {
        return await _userHttpClient.GetAsync<SellerListResponse>($"{_controller}");
    }

    public async Task<Result<SellerListResponse>> Get(SellerGetRequest request)
    {
        Dictionary<string, string> headers = new()
        {
            { "UserId", request.UserId.ToString()! }
        };
        return await _userHttpClient.GetAsync<SellerListResponse>($"{_controller}", headers);
    }

    public async Task<Result<SellerResponse?>> Get(Guid id)
    {
        return await _userHttpClient.GetAsync<SellerResponse?>($"{_controller}/{id}");
    }

    public async Task<Result<AddResponse>> Add(SellerAddRequest seller)
    {
        return await _userHttpClient.PostAsync<SellerAddRequest, AddResponse>($"{_controller}", seller);
    }

    public async Task<Result<bool>> Update(SellerUpdateRequest seller)
    {
        return await _userHttpClient.PutAsync($"{_controller}", seller);
    }

    public async Task<Result<bool>> Delete(Guid id)
    {
        return await _userHttpClient.DeleteAsync($"{_controller}/{id}");
    }
}
