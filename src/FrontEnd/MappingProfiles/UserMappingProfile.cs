using AutoMapper;
using Contracts.Requests.Seller;
using Contracts.Requests.User;
using Contracts.Responses.Seller;
using Contracts.Responses.User;

namespace FrontEnd.MappingProfiles;

/// <summary>
/// Mapper profiles
/// </summary>
public class UserMappingProfile : Profile
{
    /// <summary>
    /// Mapper profile
    /// </summary>
    public UserMappingProfile()
    {
        //source, destination (which parameters must be mapped) 
        CreateMap<UserResponse, UserUpdateRequest>(MemberList.Destination);
    }
}
