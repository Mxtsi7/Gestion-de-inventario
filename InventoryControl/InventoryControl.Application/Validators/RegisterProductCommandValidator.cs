using FluentValidation;
using InventoryControl.Application.Commands.RegisterProduct;

namespace InventoryControl.Application.Validators;

public class RegisterProductCommandValidator : AbstractValidator<RegisterProductCommand>
{
    public RegisterProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del producto es obligatorio.")
            .MaximumLength(100);

        RuleFor(x => x.Sku)
            .NotEmpty().WithMessage("El SKU es obligatorio.")
            .MaximumLength(50);

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("La categoría es obligatoria.");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a cero.");

        RuleFor(x => x.MinimumStockThreshold)
            .GreaterThanOrEqualTo(0).WithMessage("El umbral mínimo no puede ser negativo.");
    }
}
