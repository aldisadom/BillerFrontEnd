using AutoMapper;
using Contracts.Requests.Customer;
using Contracts.Requests.Item;
using Contracts.Responses.Customer;
using Contracts.Responses.Item;

namespace FrontEnd.MappingProfiles;

/// <summary>
/// Mapper profiles
/// </summary>
public class ItemMappingProfile : Profile
{
    /// <summary>
    /// Mapper profile
    /// </summary>
    public ItemMappingProfile()
    {
        //source, destination (which parameters must be mapped)
        CreateMap<ItemResponse, ItemUpdateRequest>(MemberList.Destination);
    }
}
