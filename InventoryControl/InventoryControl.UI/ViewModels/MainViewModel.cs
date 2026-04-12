using System.Collections.ObjectModel;
using System.Windows.Input;
using MediatR;
using InventoryControl.Application.Queries.GetProductList;
using InventoryControl.UI.Views;
using Microsoft.Maui.Controls;

namespace InventoryControl.UI.ViewModels;

public class MainViewModel : BindableObject
{
    private readonly IMediator _mediator;
    private ObservableCollection<ProductDto> _products = new();
    private bool _isLoading;
    private string _searchText = string.Empty;

    public ObservableCollection<ProductDto> Products
    {
        get => _products;
        set { _products = value; OnPropertyChanged(); }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set { _isLoading = value; OnPropertyChanged(); }
    }

    public string SearchText
    {
        get => _searchText;
        set { _searchText = value; OnPropertyChanged(); }
    }

    public ICommand LoadProductsCommand { get; }
    public ICommand GoToFormCommand { get; }

    public MainViewModel(IMediator mediator)
    {
        _mediator = mediator;
        LoadProductsCommand = new Command(async () => await LoadProductsAsync());
        GoToFormCommand = new Command(async () =>
        {
            if (Shell.Current is not null)
                await Shell.Current.GoToAsync(nameof(ProductFormPage));
        });
    }

    private async Task LoadProductsAsync()
    {
        IsLoading = true;
        try
        {
            var result = await _mediator.Send(new GetProductListQuery());
            Products.Clear();
            foreach (var item in result)
                Products.Add(item);
        }
        finally
        {
            IsLoading = false;
        }
    }
}
