using MediatR;
using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Application.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _repository;

    public DeleteProductHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteProductCommand command, CancellationToken ct)
        => await _repository.DeleteAsync(command.Id, ct);
}
