using Common;
using Contracts.Requests.Invoice;
using Contracts.Responses;
using Contracts.Responses.Invoice;

namespace Clients.Interfaces
{
    public interface IInvoiceClient
    {
        Task<Result<AddResponse>> Add(InvoiceAddRequest item);
        Task<Result<bool>> Delete(Guid id);
        Task<Result<InvoiceListResponse>> Get();
        Task<Result<InvoiceResponse?>> Get(Guid id);
        Task<Result<bool>> Update(InvoiceUpdateRequest item);
    }
}