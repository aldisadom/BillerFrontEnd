using Common;
using Contracts.Requests.Seller;
using Contracts.Responses;
using Contracts.Responses.Seller;

namespace Clients.Interfaces;

public interface ISellerClient
{
    Task<Result<AddResponse>> Add(SellerAddRequest seller);
    Task<Result<bool>> Delete(Guid id);
    Task<Result<SellerListResponse>> Get();
    Task<Result<SellerListResponse>> Get(SellerGetRequest request);
    Task<Result<SellerResponse?>> Get(Guid id);
    Task<Result<bool>> Update(SellerUpdateRequest seller);
}