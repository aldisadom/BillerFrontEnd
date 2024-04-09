using Clients.Interfaces;
using Contracts.Requests.InvoiceData;
using Contracts.Responses;
using Contracts.Responses.InvoiceData;
using Domain.IOptions;
using Microsoft.Extensions.Options;

namespace Clients.Clients;

public class InvoiceClient : IInvoiceClient
{
    private readonly BaseHttpClient _userHttpClient;

    public InvoiceClient(IOptions<ClientsOptions> clientOptions, IHttpClientFactory httpClientFactory)
    {
        string billioUrl = clientOptions.Value.BillioUrl
            ?? throw new ArgumentNullException($"Billio URL is missing");

        _userHttpClient = new(httpClientFactory, billioUrl);
    }
    /*
    public async Task<InvoiceDataListResponse> Get()
    {
        return await _userHttpClient.GetAsync<InvoiceDataListResponse>("Invoice");
    }

    public async Task<InvoiceDataListResponse> Get(InvoiceDataGetRequest request)
    {
        Dictionary<string, string> headers = new()
        {
            { "SellerId", request.SellerId.ToString()! }
        };
        return await _userHttpClient.GetAsync<InvoiceDataListResponse>("Invoice", headers);
    }
    */
    public async Task<InvoiceDataResponse?> Get(Guid id)
    {
        return await _userHttpClient.GetAsync<InvoiceDataResponse>($"Invoice/{id}");
    }

    public async Task<AddResponse> Add(InvoiceDataAddRequest invoice)
    {
        return await _userHttpClient.PostAsync<InvoiceDataAddRequest, AddResponse>($"Invoice", invoice);
    }

    public async Task Update(InvoiceDataUpdateRequest invoice)
    {
        await _userHttpClient.PutAsync($"Invoice", invoice);
    }

    public async Task Delete(Guid id)
    {
        await _userHttpClient.DeleteAsync($"Invoice/{id}");
    }
}
