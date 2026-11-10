using AwesomeAssertions;
using BillerContracts.Enums;
using BillerContracts.Responses.Invoice;
using FrontEnd.MappingProfiles;

namespace FrontEnd.Tests.MappingProfiles;

public class InvoiceMappingProfileTest
{
    [Fact]
    public void ToUpdateRequest_GivenInvoiceResponse_MapsAllProperties()
    {
        //Arrange
        var from = new InvoiceResponse
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            SeriesId = Guid.NewGuid(),
            SeriesName = "WF",
            InvoiceNumber = 7,
            CreatedDate = new DateOnly(2026, 11, 1),
            DueDate = new DateOnly(2026, 11, 15),
            SellerId = Guid.NewGuid(),
            SellerName = "Casterly Rock",
            CustomerId = Guid.NewGuid(),
            CustomerName = "Winterfell",
            Items =
            [
                new InvoiceItemResponse
                {
                    Id = Guid.NewGuid(),
                    Name = "Iron throne",
                    Price = 123.67M,
                    Quantity = 2,
                    Comments = "Forged from swords"
                },
                new InvoiceItemResponse
                {
                    Id = Guid.NewGuid(),
                    Name = "Valyrian sword",
                    Price = 999.99M,
                    Quantity = 1,
                    Comments = "Longclaw"
                }
            ],
            Comments = "Winter is coming",
            TotalPrice = 1247.33M,
            Status = InvoiceStatus.Send
        };

        //Act
        var to = from.ToUpdateRequest();

        //Assert
        MappingTestHelper.TestMappedAllTo(from, to, [nameof(to.Items)]);
        to.Items.Should().BeEquivalentTo(from.Items, o => o.ExcludingMissingMembers().WithStrictOrdering());
    }

    [Fact]
    public void ToUpdateRequest_GivenNullItems_ReturnsEmptyItems()
    {
        //Arrange
        var from = new InvoiceResponse
        {
            Id = Guid.NewGuid(),
            Items = null
        };

        //Act
        var to = from.ToUpdateRequest();

        //Assert
        to.Items.Should().BeEmpty();
    }

    [Fact]
    public void ToUpdateRequest_GivenInvoiceItemResponse_MapsAllProperties()
    {
        //Arrange
        var from = new InvoiceItemResponse
        {
            Id = Guid.NewGuid(),
            Name = "Iron throne",
            Price = 123.67M,
            Quantity = 2,
            Comments = "Forged from swords"
        };

        //Act
        var to = from.ToUpdateRequest();

        //Assert
        MappingTestHelper.TestMappedAllTo(from, to);
    }
}
