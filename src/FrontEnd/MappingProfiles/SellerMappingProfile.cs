using AutoMapper;
using Contracts.Requests.Customer;
using Contracts.Requests.Seller;
using Contracts.Responses.Customer;
using Contracts.Responses.Seller;

namespace FrontEnd.MappingProfiles;

/// <summary>
/// Mapper profiles
/// </summary>
public class SellerMappingProfile : Profile
{
    /// <summary>
    /// Mapper profile
    /// </summary>
    public SellerMappingProfile()
    {
        //source, destination (which parameters must be mapped)
        CreateMap<SellerResponse, SellerUpdateRequest>(MemberList.Destination);
    }
}
