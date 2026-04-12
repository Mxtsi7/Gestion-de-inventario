using System.Collections.ObjectModel;
using System.Windows.Input;
using InventoryControl.Application.Commands.DeleteProduct;
using InventoryControl.Application.Commands.RegisterProduct;
using InventoryControl.Application.DTOs;
using InventoryControl.Application.Queries;

namespace InventoryControl.UI.ViewModels;

public class ProductViewModel : BindableObject
{
    private readonly GetAllProductsHandler _getAllHandler;
    private readonly RegisterProductHandler _registerHandler;
    private readonly DeleteProductHandler _deleteHandler;

    public ObservableCollection<ProductDto> Products { get; } = new();

    public string NewName { get; set; } = string.Empty;
    public string NewSku { get; set; } = string.Empty;
    public string NewCategory { get; set; } = string.Empty;
    public decimal NewUnitPrice { get; set; }
    public int NewMinStock { get; set; }

    public ICommand LoadCommand { get; }
    public ICommand CreateCommand { get; }
    public ICommand DeleteCommand { get; }

    public ProductViewModel(
        GetAllProductsHandler getAllHandler,
        RegisterProductHandler registerHandler,
        DeleteProductHandler deleteHandler)
    {
        _getAllHandler = getAllHandler;
        _registerHandler = registerHandler;
        _deleteHandler = deleteHandler;

        LoadCommand = new Command(async () => await LoadAsync());
        CreateCommand = new Command(async () => await CreateAsync());
        DeleteCommand = new Command<Guid>(async (id) => await DeleteAsync(id));
    }

    private async Task LoadAsync()
    {
        var items = await _getAllHandler.HandleAsync();
        Products.Clear();
        foreach (var item in items)
            Products.Add(item);
    }

    private async Task CreateAsync()
    {
        var cmd = new RegisterProductCommand(NewName, NewSku, NewCategory, NewUnitPrice, NewMinStock);
        await _registerHandler.HandleAsync(cmd);
        await LoadAsync();
    }

    private async Task DeleteAsync(Guid id)
    {
        await _deleteHandler.HandleAsync(new DeleteProductCommand(id));
        await LoadAsync();
    }
}