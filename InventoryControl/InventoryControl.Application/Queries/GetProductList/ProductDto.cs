namespace InventoryControl.Application.Queries.GetProductList;

// Basic response DTO optimized for read operations
public record ProductDto(
    Guid Id,
    string Name,
    string Sku,
    string Category,
    decimal UnitPrice,
    int CurrentStock,
    string StatusBadge
);
