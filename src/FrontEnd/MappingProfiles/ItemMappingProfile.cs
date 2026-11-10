using BillerContracts.Requests.Item;
using BillerContracts.Responses.Item;

namespace FrontEnd.MappingProfiles;

/// <summary>
/// Item mapping
/// </summary>
public static class ItemResponseExtensions
{
    /// <summary>
    /// Builds the update request for the item edit page
    /// </summary>
    public static ItemUpdateRequest ToUpdateRequest(this ItemResponse from)
    {
        return new ItemUpdateRequest
        {
            Id = from.Id,
            Name = from.Name,
            Price = from.Price,
            Quantity = from.Quantity
        };
    }
}
