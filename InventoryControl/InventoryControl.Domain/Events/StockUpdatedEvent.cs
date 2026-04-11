namespace InventoryControl.Domain.Events;

public record StockUpdatedEvent(Guid ProductId, int OldStock, int NewStock, DateTime OccurredAt)
{
    public StockUpdatedEvent(Guid productId, int oldStock, int newStock)
        : this(productId, oldStock, newStock, DateTime.UtcNow) { }
}
