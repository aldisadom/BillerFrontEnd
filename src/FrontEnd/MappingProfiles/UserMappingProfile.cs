using BillerContracts.Requests.User;
using BillerContracts.Responses.User;

namespace FrontEnd.MappingProfiles;

/// <summary>
/// User mapping
/// </summary>
public static class UserResponseExtensions
{
    /// <summary>
    /// Builds the update request for the user edit page
    /// </summary>
    public static UserUpdateRequest ToUpdateRequest(this UserResponse from)
    {
        return new UserUpdateRequest
        {
            Id = from.Id,
            Name = from.Name,
            LastName = from.LastName
        };
    }
}
