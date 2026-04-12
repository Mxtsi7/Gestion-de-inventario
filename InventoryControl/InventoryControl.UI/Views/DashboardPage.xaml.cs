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
        // CommunityToolkit.Mvvm elimina el sufijo Async:
        // método CargarDashboardAsync() → genera CargarDashboardCommand
        if (BindingContext is DashboardViewModel vm)
            vm.CargarDashboardCommand.Execute(null);
    }
}