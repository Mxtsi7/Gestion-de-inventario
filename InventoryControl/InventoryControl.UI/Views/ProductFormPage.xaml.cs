using InventoryControl.UI.ViewModels;

namespace InventoryControl.UI.Views;

public partial class ProductFormPage : ContentPage
{
    public ProductFormPage(ProductFormViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
