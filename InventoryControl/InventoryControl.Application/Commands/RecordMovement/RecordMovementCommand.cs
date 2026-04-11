using MediatR;

namespace InventoryControl.Application.Commands.RecordMovement;

public enum MovementType
{
    Entry,
    Exit
}

public record RecordMovementCommand(
    Guid ProductId,
    int Quantity,
    MovementType Type
) : IRequest<bool>;