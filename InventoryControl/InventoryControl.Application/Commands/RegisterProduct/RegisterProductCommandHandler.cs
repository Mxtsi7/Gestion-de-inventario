using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Application.Commands.RegisterProduct;

public class RegisterProductHandler
{
    private readonly IProductRepository _repository;

    public RegisterProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> HandleAsync(RegisterProductCommand command, CancellationToken ct = default)
    {
        var product = new Product(
            command.Name,
            command.Sku,
            command.Category,
            command.UnitPrice,
            command.MinimumStockThreshold
        );
        await _repository.AddAsync(product, ct);
        return product.Id;
    }
}