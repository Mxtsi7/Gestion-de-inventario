using MediatR;

namespace InventoryControl.Application.Commands.RegisterProduct;

// Command DTO holding strictly information for execution
public record RegisterProductCommand(
    string Name,
    string Sku,
    string Category,
    decimal UnitPrice,
    int MinimumStockThreshold
) : IRequest<Guid>;