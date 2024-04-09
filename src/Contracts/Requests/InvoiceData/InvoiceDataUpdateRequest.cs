using Contracts.Responses.Customer;
using Contracts.Responses.InvoiceData;
using Contracts.Responses.Seller;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Requests.InvoiceData;

public class InvoiceDataUpdateRequest
{
    public DateTime CreatedDate { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public SellerResponse Seller { get; set; }
    [Required]
    public CustomerResponse Customer { get; set; }
    [Required]
    public List<InvoiceItemResponse>? Items { get; set; }
    public string? Comments { get; set; }
    public decimal TotalPrice { get; set; }
}
