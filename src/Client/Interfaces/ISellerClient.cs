using Contracts.Requests.Seller;
using Contracts.Responses;
using Contracts.Responses.Seller;

namespace Clients.Interfaces;

public interface ISellerClient
{
    Task<AddResponse> Add(SellerAddRequest seller);
    Task Delete(Guid id);
    Task<SellerListResponse> Get();
    Task<SellerListResponse> Get(SellerGetRequest request);
    Task<SellerResponse?> Get(Guid id);
    Task Update(SellerUpdateRequest seller);
}