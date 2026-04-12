using System.Windows.Input;
using MediatR;
using InventoryControl.Application.Commands.RegisterProduct;
using Microsoft.Maui.Controls;

namespace InventoryControl.UI.ViewModels;

public class ProductFormViewModel : BindableObject
{
    private readonly IMediator _mediator;
    private string _name = string.Empty;
    private string _sku = string.Empty;
    private string _category = string.Empty;
    private string _unitPrice = string.Empty;
    private string _minimumStockThreshold = string.Empty;
    private string _errorMessage = string.Empty;
    private bool _isBusy;

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }

    public string Sku
    {
        get => _sku;
        set { _sku = value; OnPropertyChanged(); }
    }

    public string Category
    {
        get => _category;
        set { _category = value; OnPropertyChanged(); }
    }

    public string UnitPrice
    {
        get => _unitPrice;
        set { _unitPrice = value; OnPropertyChanged(); }
    }

    public string MinimumStockThreshold
    {
        get => _minimumStockThreshold;
        set { _minimumStockThreshold = value; OnPropertyChanged(); }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set { _errorMessage = value; OnPropertyChanged(); OnPropertyChanged(nameof(HasError)); }
    }

    public bool HasError => !string.IsNullOrEmpty(_errorMessage);
    public bool IsNotBusy => !_isBusy;

    public bool IsBusy
    {
        get => _isBusy;
        set { _isBusy = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsNotBusy)); }
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public ProductFormViewModel(IMediator mediator)
    {
        _mediator = mediator;
        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    private async Task SaveAsync()
    {
        ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Sku) || string.IsNullOrWhiteSpace(Category))
        {
            ErrorMessage = "Nombre, SKU y Categoría son obligatorios.";
            return;
        }

        if (!decimal.TryParse(UnitPrice, out var price) || price < 0)
        {
            ErrorMessage = "El precio debe ser un número válido.";
            return;
        }

        if (!int.TryParse(MinimumStockThreshold, out var minStock) || minStock < 0)
        {
            ErrorMessage = "El stock mínimo debe ser un número entero válido.";
            return;
        }

        IsBusy = true;
        try
        {
            var command = new RegisterProductCommand(Name, Sku, Category, price, minStock);
            await _mediator.Send(command);
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error al guardar: {ex.Message}";
        }
        finally
        {
            IsBusy = false;
        }
    }
}
