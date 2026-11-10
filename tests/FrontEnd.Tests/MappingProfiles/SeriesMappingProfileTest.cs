using BillerContracts.Responses.Series;
using FrontEnd.MappingProfiles;

namespace FrontEnd.Tests.MappingProfiles;

public class SeriesMappingProfileTest
{
    [Fact]
    public void ToUpdateRequest_GivenResponse_MapsAllProperties()
    {
        //Arrange
        var from = new SeriesResponse
        {
            Id = Guid.NewGuid(),
            SellerId = Guid.NewGuid(),
            Name = "WF",
            NextNumber = 42
        };

        //Act
        var to = from.ToUpdateRequest();

        //Assert
        MappingTestHelper.TestMappedAllTo(from, to);
    }
}
