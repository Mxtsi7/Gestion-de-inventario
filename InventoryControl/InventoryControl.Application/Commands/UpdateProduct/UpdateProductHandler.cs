using MediatR;
using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Application.Commands.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _repository;

    public UpdateProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateProductCommand command, CancellationToken ct)
    {
        var product = await _repository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Product {command.Id} not found.");

        product.Update(command.Name, command.Category, command.UnitPrice, command.MinimumStockThreshold);

        await _repository.UpdateAsync(product, ct);
    }
}
