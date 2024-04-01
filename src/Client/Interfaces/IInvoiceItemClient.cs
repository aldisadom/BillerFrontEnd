using Contracts.Requests.Item;
using Contracts.Responses;
using Contracts.Responses.Item;

namespace Clients.Interfaces;

public interface IItemClient
{
    Task<AddResponse> Add(ItemAddRequest item);
    Task Delete(Guid id);
    Task<ItemListResponse> Get();
    Task<ItemResponse?> Get(Guid id);
    Task<ItemListResponse> Get(ItemGetRequest request);
    Task Update(ItemUpdateRequest item);
}