using BillerContracts.Responses.Seller;
using FrontEnd.MappingProfiles;

namespace FrontEnd.Tests.MappingProfiles;

public class SellerMappingProfileTest
{
    [Fact]
    public void ToUpdateRequest_GivenResponse_MapsAllProperties()
    {
        //Arrange
        var from = new SellerResponse
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompanyNumber = "987654321",
            CompanyName = "Casterly Rock",
            Street = "Lion street 3",
            City = "Lannisport",
            State = "The Westerlands",
            Email = "tywin@casterlyrock.com",
            Phone = "+37061111111",
            BankName = "Iron bank of Braavos",
            BankNumber = "LT123456789012345678"
        };

        //Act
        var to = from.ToUpdateRequest();

        //Assert
        MappingTestHelper.TestMappedAllTo(from, to);
    }
}
