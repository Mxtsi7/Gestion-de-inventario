using MediatR;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Application.Commands.RegisterProduct;

// Business logic executing the instruction
public class RegisterProductCommandHandler : IRequestHandler<RegisterProductCommand, Guid>
{
    private readonly IProductRepository _repository;

    public RegisterProductCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(RegisterProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(
            request.Name,
            request.Sku,
            request.Category,
            request.UnitPrice,
            request.MinimumStockThreshold
        );

        await _repository.AddAsync(product, cancellationToken);

        return product.Id;
    }
}