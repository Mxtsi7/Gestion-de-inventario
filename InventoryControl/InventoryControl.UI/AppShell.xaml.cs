using InventoryControl.UI.Views;

namespace InventoryControl.UI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(ProductFormPage), typeof(ProductFormPage));
    }
}
