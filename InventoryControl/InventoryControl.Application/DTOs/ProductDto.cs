namespace InventoryControl.Application.DTOs;

public record ProductDto(
    Guid Id,
    string Name,
    string Sku,
    string Category,
    decimal UnitPrice,
    int CurrentStock,
    int MinimumStockThreshold,
    string StatusBadge
);
