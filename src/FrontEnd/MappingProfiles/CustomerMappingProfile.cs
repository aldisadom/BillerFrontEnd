using AutoMapper;
using Contracts.Requests.Customer;
using Contracts.Responses.Customer;

namespace FrontEnd.MappingProfiles;

/// <summary>
/// Mapper profiles
/// </summary>
public class CustomerMappingProfile : Profile
{
    /// <summary>
    /// Mapper profile
    /// </summary>
    public CustomerMappingProfile()
    {
        //source, destination (which parameters must be mapped)
        CreateMap<CustomerResponse, CustomerUpdateRequest>(MemberList.Destination);
    }
}
