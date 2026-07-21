using AutoMapper;
using BillerContracts.Requests.Item;
using BillerContracts.Responses.Item;

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
