using AutoMapper;
using Contracts.Requests.Invoice;
using Contracts.Responses.Invoice;

namespace FrontEnd.MappingProfiles;

/// <summary>
/// Mapper profiles
/// </summary>
public class InvoiceMappingProfile : Profile
{
    /// <summary>
    /// Mapper profile
    /// </summary>
    public InvoiceMappingProfile()
    {
        //source, destination (which parameters must be mapped)
        CreateMap<InvoiceItemResponse, InvoiceItemUpdateRequest>(MemberList.Destination);

        CreateMap<InvoiceResponse, InvoiceUpdateRequest>(MemberList.Destination);
    }
}
