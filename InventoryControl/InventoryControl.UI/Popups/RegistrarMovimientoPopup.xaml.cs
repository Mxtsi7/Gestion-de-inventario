using CommunityToolkit.Maui.Views;
using InventoryControl.UI.ViewModels;

namespace InventoryControl.UI.Popups;

public partial class RegistrarMovimientoPopup : Popup
{
    public RegistrarMovimientoPopup(RegistrarMovimientoViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        vm.CerrarAction = () => Close();
    }
}