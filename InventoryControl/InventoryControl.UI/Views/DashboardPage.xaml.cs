using InventoryControl.UI.ViewModels;

namespace InventoryControl.UI.Views;

public partial class DashboardPage : ContentPage
{
    public DashboardPage(DashboardViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is DashboardViewModel vm)
            vm.CargarDashboardAsyncCommand.Execute(null);
    }
}