using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Application.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id);

public class DeleteProductHandler
{
    private readonly IProductRepository _repository;

    public DeleteProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(DeleteProductCommand command, CancellationToken ct = default)
        => await _repository.DeleteAsync(command.Id, ct);
}