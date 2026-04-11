namespace InventoryControl.Domain.Events;

public record LowStockWarningEvent(Guid ProductId, string ProductName, int CurrentStock, int Threshold)
{
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
}
