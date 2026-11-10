using BillerContracts.Responses.Item;
using FrontEnd.MappingProfiles;

namespace FrontEnd.Tests.MappingProfiles;

public class ItemMappingProfileTest
{
    [Fact]
    public void ToUpdateRequest_GivenResponse_MapsAllProperties()
    {
        //Arrange
        var from = new ItemResponse
        {
            Id = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            Name = "Iron throne",
            Price = 123.67M,
            Quantity = 66
        };

        //Act
        var to = from.ToUpdateRequest();

        //Assert
        MappingTestHelper.TestMappedAllTo(from, to);
    }
}
