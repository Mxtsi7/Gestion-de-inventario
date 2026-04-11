using System.Windows.Input;
using MediatR;
using InventoryControl.Application.Commands.RegisterProduct;

namespace InventoryControl.UI.ViewModels;

public class ProductFormViewModel : BindableObject
{
    private readonly IMediator _mediator;

    public string Name { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int MinimumStockThreshold { get; set; }

    public ICommand SaveCommand { get; }

    public ProductFormViewModel(IMediator mediator)
    {
        _mediator = mediator;
        SaveCommand = new Command(async () => await SaveAsync());
    }

    private async Task SaveAsync()
    {
        var command = new RegisterProductCommand(Name, Sku, Category, UnitPrice, MinimumStockThreshold);
        await _mediator.Send(command);
        await Shell.Current.GoToAsync("..");
    }
}
