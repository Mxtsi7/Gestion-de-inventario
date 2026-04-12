namespace InventoryControl.Application.Commands.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Category,
    decimal UnitPrice,
    int MinimumStockThreshold
);