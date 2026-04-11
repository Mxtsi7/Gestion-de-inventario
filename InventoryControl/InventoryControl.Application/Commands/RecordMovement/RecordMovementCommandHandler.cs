using MediatR;
using InventoryControl.Domain.Interfaces;

namespace InventoryControl.Application.Commands.RecordMovement;

public class RecordMovementCommandHandler : IRequestHandler<RecordMovementCommand, bool>
{
    private readonly IProductRepository _repository;

    public RecordMovementCommandHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(RecordMovementCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product == null) return false;

        if (request.Type == MovementType.Entry)
            product.AddStock(request.Quantity);
        else
            product.RemoveStock(request.Quantity);

        await _repository.UpdateAsync(product, cancellationToken);
        return true;
    }
}