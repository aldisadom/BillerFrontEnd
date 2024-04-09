﻿using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.InvoiceData;

public class InvoiceDataGenerateRequest
{
    [Required]
    public Guid SellerId { get; set; }
    [Required]
    public Guid CustomerId { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    [MinLength(1)]
    public List<Guid> ItemsId { get; set; } = new();
}
