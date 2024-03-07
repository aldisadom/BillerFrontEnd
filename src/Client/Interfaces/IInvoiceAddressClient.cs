using Contracts.Requests.InvoiceAddress;
using Contracts.Responses;
using Contracts.Responses.InvoiceAddress;

namespace Clients.Interfaces
{
    public interface IInvoiceAddressClient
    {
        Task<AddResponse> Add(InvoiceAddressAddRequest user);
        Task Delete(Guid id);
        Task<InvoiceAddressListResponse> Get();
        Task<InvoiceAddressListResponse> Get(InvoiceAddressGetRequest request);
        Task<InvoiceAddressResponse?> Get(Guid id);
        Task Update(InvoiceAddressUpdateRequest user);
    }
}