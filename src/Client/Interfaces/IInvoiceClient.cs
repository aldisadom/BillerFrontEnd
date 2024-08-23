using Contracts.Requests.Invoice;
using Contracts.Responses;
using Contracts.Responses.Invoice;

namespace Clients.Interfaces
{
    public interface IInvoiceClient
    {
        Task<AddResponse> Add(InvoiceAddRequest item);
        Task Delete(Guid id);
        Task<InvoiceListResponse> Get();
        Task<InvoiceResponse?> Get(Guid id);
        Task Update(InvoiceUpdateRequest item);
    }
}