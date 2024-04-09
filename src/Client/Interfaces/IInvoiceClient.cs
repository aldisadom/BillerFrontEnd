using Contracts.Requests.InvoiceData;
using Contracts.Responses;
using Contracts.Responses.InvoiceData;

namespace Clients.Interfaces
{
    public interface IInvoiceClient
    {
        Task<AddResponse> Add(InvoiceDataAddRequest invoice);
        Task Delete(Guid id);
        Task<InvoiceDataResponse?> Get(Guid id);
        Task Update(InvoiceDataUpdateRequest invoice);
    }
}