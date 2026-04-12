using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Application.Commands.UpdateProduct;

public class UpdateProductHandler
{
    private readonly IProductRepository _repository;

    public UpdateProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UpdateProductCommand command, CancellationToken ct = default)
    {
        var product = await _repository.GetByIdAsync(command.Id, ct)
            ?? throw new KeyNotFoundException($"Product {command.Id} not found.");

        // Usa reflexión o agrega un método Update en la entidad:
        typeof(Domain.Entities.Product)
            .GetProperty("Name")!.SetValue(product, command.Name);
        typeof(Domain.Entities.Product)
            .GetProperty("Category")!.SetValue(product, command.Category);
        typeof(Domain.Entities.Product)
            .GetProperty("UnitPrice")!.SetValue(product, command.UnitPrice);
        typeof(Domain.Entities.Product)
            .GetProperty("MinimumStockThreshold")!.SetValue(product, command.MinimumStockThreshold);

        await _repository.UpdateAsync(product, ct);
    }
}