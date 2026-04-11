namespace InventoryControl.Application.DTOs;

public record MovementHistoryDto(
    Guid Id,
    Guid ProductId,
    string ProductName,
    int Quantity,
    string MovementType,
    DateTime OccurredAt
);
