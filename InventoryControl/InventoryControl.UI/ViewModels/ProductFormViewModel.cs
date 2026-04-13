using System.Windows.Input;
using MediatR;
using InventoryControl.Application.Commands.RegisterProduct;
using InventoryControl.Application.Commands.UpdateProduct;
using InventoryControl.Domain.Interfaces;
using Microsoft.Maui.Controls;

namespace InventoryControl.UI.ViewModels;

[QueryProperty(nameof(ProductId), "id")]
public class ProductFormViewModel : BindableObject
{
    private readonly IMediator _mediator;
    private readonly IProductRepository _repository;

    private string _productId = string.Empty;
    private string _name = string.Empty;
    private string _sku = string.Empty;
    private string _category = string.Empty;
    private string _unitPrice = string.Empty;
    private string _minimumStockThreshold = string.Empty;
    private string _errorMessage = string.Empty;
    private bool _isBusy;

    // Cuando Shell asigna el id, cargamos el producto existente
    public string ProductId
    {
        get => _productId;
        set
        {
            _productId = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsEditMode));
            OnPropertyChanged(nameof(PageTitle));
            if (Guid.TryParse(value, out _))
                _ = LoadProductAsync(value);
        }
    }

    public bool IsEditMode => Guid.TryParse(_productId, out _);
    public string PageTitle => IsEditMode ? "Editar Producto" : "Nuevo Producto";

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

    public ProductFormViewModel(IMediator mediator, IProductRepository repository)
    {
        _mediator = mediator;
        _repository = repository;
        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    private async Task LoadProductAsync(string id)
    {
        if (!Guid.TryParse(id, out var guid)) return;
        var product = await _repository.GetByIdAsync(guid);
        if (product is null) return;

        Name = product.Name;
        Sku = product.Sku;
        Category = product.Category;
        UnitPrice = product.UnitPrice.ToString();
        MinimumStockThreshold = product.MinimumStockThreshold.ToString();
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
            if (IsEditMode)
            {
                var command = new UpdateProductCommand(Guid.Parse(_productId), Name, Category, price, minStock);
                await _mediator.Send(command);
            }
            else
            {
                var command = new RegisterProductCommand(Name, Sku, Category, price, minStock);
                await _mediator.Send(command);
            }

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
