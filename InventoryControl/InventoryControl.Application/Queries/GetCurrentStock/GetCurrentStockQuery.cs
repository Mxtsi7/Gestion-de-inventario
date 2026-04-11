using MediatR;
using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Application.Queries.GetCurrentStock;

public record GetCurrentStockQuery(Guid ProductId) : IRequest<int?>;

public class GetCurrentStockQueryHandler : IRequestHandler<GetCurrentStockQuery, int?>
{
    private readonly IProductRepository _repository;

    public GetCurrentStockQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<int?> Handle(GetCurrentStockQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.ProductId, cancellationToken);
        return product?.CurrentStock;
    }
}
