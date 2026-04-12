using InventoryControl.UI.ViewModels;

namespace InventoryControl.UI.Views;

public partial class ProductosPage : ContentPage
{
    public ProductosPage(ProductosViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // método CargarProductos() → genera CargarProductosCommand
        if (BindingContext is ProductosViewModel vm)
            vm.CargarProductosCommand.Execute(null);
    }
}