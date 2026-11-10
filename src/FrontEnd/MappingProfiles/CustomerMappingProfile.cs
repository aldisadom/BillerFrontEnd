using BillerContracts.Requests.Customer;
using BillerContracts.Responses.Customer;

namespace FrontEnd.MappingProfiles;

/// <summary>
/// Customer mapping
/// </summary>
public static class CustomerResponseExtensions
{
    /// <summary>
    /// Builds the update request for the customer edit page
    /// </summary>
    public static CustomerUpdateRequest ToUpdateRequest(this CustomerResponse from)
    {
        return new CustomerUpdateRequest
        {
            Id = from.Id,
            SeriesId = from.SeriesId,
            CompanyName = from.CompanyName,
            CompanyNumber = from.CompanyNumber,
            Street = from.Street,
            City = from.City,
            State = from.State,
            Email = from.Email,
            Phone = from.Phone
        };
    }
}
