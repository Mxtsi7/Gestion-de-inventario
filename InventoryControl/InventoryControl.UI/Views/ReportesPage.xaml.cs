using InventoryControl.UI.ViewModels;

namespace InventoryControl.UI.Views;

public partial class ReportesPage : ContentPage
{
    public ReportesPage(ReportesViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ReportesViewModel vm)
            vm.GenerarReporteCommand.Execute(null);
    }
}