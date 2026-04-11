namespace InventoryControl.Application.Queries.GetProductList;

// Basic response DTO optimized for read operations
public record ProductDto(
    Guid Id,
    string Name,
    string Sku,
    int CurrentStock,
    string StatusBadge
);