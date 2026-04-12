using InventoryControl.UI.ViewModels;

namespace InventoryControl.UI.Views;

public partial class ReportsPage : ContentPage
{
    public ReportsPage(ReportsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ReportsViewModel vm)
            vm.LoadReportsCommand.Execute(null);
    }
}
