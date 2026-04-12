using InventoryControl.UI.ViewModels;

namespace InventoryControl.UI.Views;

public partial class MovimientosPage : ContentPage
{
    public MovimientosPage(MovimientosViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MovimientosViewModel vm)
            vm.CargarMovimientosCommand.Execute(null);
    }
}