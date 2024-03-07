using Contracts.Requests.InvoiceItem;
using Contracts.Responses;
using Contracts.Responses.InvoiceItem;

namespace Clients.Interfaces
{
    public interface IInvoiceItemClient
    {
        Task<AddResponse> Add(InvoiceItemAddRequest user);
        Task Delete(Guid id);
        Task<InvoiceItemListResponse> Get();
        Task<InvoiceItemResponse?> Get(Guid id);
        Task<InvoiceItemListResponse> Get(InvoiceItemGetRequest request);
        Task Update(InvoiceItemUpdateRequest user);
    }
}