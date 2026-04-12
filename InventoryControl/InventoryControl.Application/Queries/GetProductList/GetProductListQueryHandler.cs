using MediatR;
using InventoryControl.Application.DTOs;
using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Application.Queries.GetProductList;

public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, List<ProductDto>>
{
    private readonly IProductRepository _repository;

    public GetProductListQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProductDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync(cancellationToken);

        return products
            .Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Sku,
                p.Category,
                p.UnitPrice,
                p.MinimumStockThreshold,
                p.CurrentStock,
                p.GetStockLevelStatus()
            ))
            .ToList();
    }
}
