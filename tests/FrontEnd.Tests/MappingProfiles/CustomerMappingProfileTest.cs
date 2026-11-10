using BillerContracts.Responses.Customer;
using FrontEnd.MappingProfiles;

namespace FrontEnd.Tests.MappingProfiles;

public class CustomerMappingProfileTest
{
    [Fact]
    public void ToUpdateRequest_GivenResponse_MapsAllProperties()
    {
        //Arrange
        var from = new CustomerResponse
        {
            Id = Guid.NewGuid(),
            SellerId = Guid.NewGuid(),
            SeriesId = Guid.NewGuid(),
            SeriesName = "WF",
            CompanyNumber = "123456789",
            CompanyName = "Winterfell",
            Street = "Kings road 1",
            City = "Winterfell",
            State = "The North",
            Email = "ned@winterfell.com",
            Phone = "+37060000000"
        };

        //Act
        var to = from.ToUpdateRequest();

        //Assert
        MappingTestHelper.TestMappedAllTo(from, to);
    }
}
