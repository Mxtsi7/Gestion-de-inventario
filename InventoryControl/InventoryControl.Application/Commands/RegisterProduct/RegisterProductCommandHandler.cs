using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using MediatR;

namespace InventoryControl.Application.Commands.RegisterProduct;

public class RegisterProductHandler : IRequestHandler<RegisterProductCommand, Guid>
{
    private readonly IProductRepository _repository;

    public RegisterProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(RegisterProductCommand command, CancellationToken ct)
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
