using Clients.Interfaces;
using Contracts.Requests.Invoice;
using Contracts.Responses;
using Contracts.Responses.Invoice;
using Domain.IOptions;
using Microsoft.Extensions.Options;

namespace Clients.Clients;

public class InvoiceClient : IInvoiceClient
{
    private readonly BaseHttpClient _userHttpClient;
    private readonly string _controller = "invoices";

    public InvoiceClient(IOptions<ClientsOptions> clientOptions, IHttpClientFactory httpClientFactory)
    {
        string billioUrl = clientOptions.Value.BillioUrl
            ?? throw new ArgumentNullException($"URL is missing {nameof(clientOptions.Value.BillioUrl)}");

        _userHttpClient = new(httpClientFactory, billioUrl);
    }

    public async Task<InvoiceListResponse> Get()
    {
        return await _userHttpClient.GetAsync<InvoiceListResponse>($"{_controller}");
    }
    /*
    public async Task<InvoiceDataListResponse> Get(InvoiceDataGetRequest request)
    {
        Dictionary<string, string> headers = new()
        {
            { "SellerId", request.SellerId.ToString()! }
        };
        return await _userHttpClient.GetAsync<InvoiceDataListResponse>("{_controller}", headers);
    }
    */
    public async Task<InvoiceResponse?> Get(Guid id)
    {
        return await _userHttpClient.GetAsync<InvoiceResponse>($"{_controller}/{id}");
    }

    public async Task<AddResponse> Add(InvoiceAddRequest invoice)
    {
        return await _userHttpClient.PostAsync<InvoiceAddRequest, AddResponse>($"{_controller}", invoice);
    }

    public async Task Update(InvoiceUpdateRequest invoice)
    {
        await _userHttpClient.PutAsync($"{_controller}", invoice);
    }

    public async Task Delete(Guid id)
    {
        await _userHttpClient.DeleteAsync($"{_controller}/{id}");
    }

    public async Task<FileDownloadResult> GeneratePDF(InvoiceGenerateRequest invoice)
    {
        return await _userHttpClient.PostFileDowload($"{_controller}/generate", invoice);
    }

    public class FileDownloadResult
    {
        public Stream Content { get; set; } = default!;
        public string? FileName { get; set; }
        public string? ContentType { get; set; }
    }
}
