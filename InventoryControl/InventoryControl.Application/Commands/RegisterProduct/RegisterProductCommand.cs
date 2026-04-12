using MediatR;

namespace InventoryControl.Application.Commands.RegisterProduct;

public record RegisterProductCommand(
    string Name,
    string Sku,
    string Category,
    decimal UnitPrice,
    int MinimumStockThreshold
) : IRequest<Guid>;
