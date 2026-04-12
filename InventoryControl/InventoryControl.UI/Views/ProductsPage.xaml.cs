using InventoryControl.UI.ViewModels;

namespace InventoryControl.UI.Views;

public partial class ProductsPage : ContentPage
{
    public ProductsPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MainViewModel vm)
            vm.LoadProductsCommand.Execute(null);
    }
}
