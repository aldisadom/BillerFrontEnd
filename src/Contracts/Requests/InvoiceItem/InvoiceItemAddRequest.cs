﻿using System.ComponentModel.DataAnnotations;

namespace Contracts.Requests.InvoiceItem;

public record InvoiceItemAddRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public decimal Price { get; set; }
    [Required]
    public Guid ClientId { get; set; }
}