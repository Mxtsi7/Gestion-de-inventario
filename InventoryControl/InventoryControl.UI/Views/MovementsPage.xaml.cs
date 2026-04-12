using InventoryControl.UI.ViewModels;

namespace InventoryControl.UI.Views;

public partial class MovementsPage : ContentPage
{
    public MovementsPage(MovementsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MovementsViewModel vm)
            vm.LoadMovementsCommand.Execute(null);
    }
}
