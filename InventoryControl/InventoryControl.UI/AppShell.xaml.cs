using InventoryControl.UI.Views;

namespace InventoryControl.UI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("productForm", typeof(ProductFormPage));
    }
}
