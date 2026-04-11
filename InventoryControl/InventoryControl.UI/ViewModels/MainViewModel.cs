using System.Collections.ObjectModel;
using System.Windows.Input;
using MediatR;
using InventoryControl.Application.Queries.GetProductList;
using Microsoft.Maui.Controls;

namespace InventoryControl.UI.ViewModels;

public class MainViewModel : BindableObject
{
    private readonly IMediator _mediator;
    private ObservableCollection<ProductDto> _products;

    public ObservableCollection<ProductDto> Products
    {
        get => _products;
        set { _products = value; OnPropertyChanged(); }
    }

    public ICommand LoadProductsCommand { get; }

    public MainViewModel(IMediator mediator)
    {
        _mediator = mediator;
        Products = new ObservableCollection<ProductDto>();
        LoadProductsCommand = new Command(async () => await LoadProductsAsync());
    }

    private async Task LoadProductsAsync()
    {
        var result = await _mediator.Send(new GetProductListQuery());

        Products.Clear();
        foreach (var item in result)
        {
            Products.Add(item);
        }
    }
}