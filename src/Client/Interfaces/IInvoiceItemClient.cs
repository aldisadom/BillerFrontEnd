using Common;
using Contracts.Requests.Item;
using Contracts.Responses;
using Contracts.Responses.Item;

namespace Clients.Interfaces;

public interface IItemClient
{
    Task<Result<AddResponse>> Add(ItemAddRequest item);
    Task<Result<bool>> Delete(Guid id);
    Task<Result<ItemListResponse>> Get();
    Task<Result<ItemResponse?>> Get(Guid id);
    Task<Result<ItemListResponse>> Get(ItemGetRequest request);
    Task<Result<bool>> Update(ItemUpdateRequest item);
}