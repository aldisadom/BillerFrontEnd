using BillerContracts.Responses.User;
using FrontEnd.MappingProfiles;

namespace FrontEnd.Tests.MappingProfiles;

public class UserMappingProfileTest
{
    [Fact]
    public void ToUpdateRequest_GivenResponse_MapsAllProperties()
    {
        //Arrange
        var from = new UserResponse
        {
            Id = Guid.NewGuid(),
            Name = "Jon",
            LastName = "Snow",
            Email = "jon@nightswatch.com"
        };

        //Act
        var to = from.ToUpdateRequest();

        //Assert
        MappingTestHelper.TestMappedAllTo(from, to);
    }
}
