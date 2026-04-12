using InventoryControl.Application.DTOs;
using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Application.Queries;

public class GetProductByIdHandler
{
    private readonly IProductRepository _repository;

    public GetProductByIdHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductDto?> HandleAsync(Guid id, CancellationToken ct = default)
    {
        var p = await _repository.GetByIdAsync(id, ct);
        if (p is null) return null;

        return new ProductDto(
            p.Id, p.Name, p.Sku, p.Category,
            p.UnitPrice, p.MinimumStockThreshold,
            p.CurrentStock, p.GetStockLevelStatus()
        );
    }
}