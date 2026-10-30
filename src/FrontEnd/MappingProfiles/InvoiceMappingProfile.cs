using BillerContracts.Requests.Invoice;
using BillerContracts.Responses.Invoice;

namespace FrontEnd.MappingProfiles;

public static class InvoiceResponseExtensions
{
    public static InvoiceUpdateRequest ToUpdateRequest(this InvoiceResponse from)
    {
        return new InvoiceUpdateRequest
        {
            Id = from.Id,
            SellerId = from.SellerId,
            CustomerId = from.CustomerId,
            Items = from.Items?.Select(i => i.ToUpdateRequest()).ToList() ?? [],
            Comments = from.Comments,
            DueDate = from.DueDate,
            CreatedDate = from.CreatedDate
        };
    }

    public static InvoiceItemRequest ToUpdateRequest(this InvoiceItemResponse from)
    {
        return new InvoiceItemRequest
        {
            Id = from.Id,
            Quantity = from.Quantity,
            Comments = from.Comments
        };
    }
}
