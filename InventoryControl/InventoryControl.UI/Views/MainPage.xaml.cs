using InventoryControl.UI.ViewModels;

namespace InventoryControl.UI.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MainViewModel vm)
            await vm.LoadProductsCommand.ExecuteAsync(null);
    }
}
