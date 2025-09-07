using Contracts.Requests.Invoice;
using Contracts.Responses;
using Contracts.Responses.Invoice;
using static Clients.Clients.InvoiceClient;

namespace Clients.Interfaces
{
    public interface IInvoiceClient
    {
        Task<AddResponse> Add(InvoiceAddRequest invoice);
        Task Delete(Guid id);
        Task<InvoiceListResponse> Get();
        Task<InvoiceResponse?> Get(Guid id);
        Task Update(InvoiceUpdateRequest invoice);
        Task UpdateStatus(InvoiceUpdateStatusRequest invoice);
        Task<FileDownloadResult> GeneratePDF(InvoiceGenerateRequest invoice);
    }
}