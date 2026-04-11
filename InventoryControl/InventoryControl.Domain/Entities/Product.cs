namespace InventoryControl.Domain.Entities;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Sku { get; private set; } = string.Empty;
    public string Category { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }
    public int MinimumStockThreshold { get; private set; }
    public int CurrentStock { get; private set; }

    // Constructor privado requerido por EF Core
    private Product() { }

    public Product(string name, string sku, string category, decimal unitPrice, int minimumStockThreshold)
    {
        Id = Guid.NewGuid();
        Name = name;
        Sku = sku;
        Category = category;
        UnitPrice = unitPrice;
        MinimumStockThreshold = minimumStockThreshold;
        CurrentStock = 0;
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be positive.");
        CurrentStock += quantity;
    }

    public void RemoveStock(int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantity must be positive.");
        if (CurrentStock - quantity < 0) throw new InvalidOperationException("Insufficient stock to cover exit.");
        CurrentStock -= quantity;
    }

    public string GetStockLevelStatus()
    {
        if (CurrentStock == 0) return "OUT";
        if (CurrentStock <= MinimumStockThreshold) return "LOW";
        return "OK";
    }
}
