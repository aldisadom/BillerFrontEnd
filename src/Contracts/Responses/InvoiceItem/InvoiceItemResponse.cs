﻿namespace Contracts.Responses.InvoiceItem;

public record InvoiceItemResponse
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}