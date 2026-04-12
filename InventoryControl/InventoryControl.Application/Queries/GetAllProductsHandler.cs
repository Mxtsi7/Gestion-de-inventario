using InventoryControl.Application.DTOs;
using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Application.Queries;

public class GetAllProductsHandler
{
    private readonly IProductRepository _repository;

    public GetAllProductsHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductDto>> HandleAsync(CancellationToken ct = default)
    {
        var products = await _repository.GetAllAsync(ct);
        return products.Select(p => new ProductDto(
            p.Id, p.Name, p.Sku, p.Category,
            p.UnitPrice, p.MinimumStockThreshold,
            p.CurrentStock, p.GetStockLevelStatus()
        ));
    }
}