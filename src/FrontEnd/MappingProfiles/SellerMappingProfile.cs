using BillerContracts.Requests.Seller;
using BillerContracts.Responses.Seller;

namespace FrontEnd.MappingProfiles;

/// <summary>
/// Seller mapping
/// </summary>
public static class SellerResponseExtensions
{
    /// <summary>
    /// Builds the update request for the seller edit page
    /// </summary>
    public static SellerUpdateRequest ToUpdateRequest(this SellerResponse from)
    {
        return new SellerUpdateRequest
        {
            Id = from.Id,
            CompanyNumber = from.CompanyNumber,
            CompanyName = from.CompanyName,
            Street = from.Street,
            City = from.City,
            State = from.State,
            Email = from.Email,
            Phone = from.Phone,
            BankName = from.BankName,
            BankNumber = from.BankNumber
        };
    }
}
