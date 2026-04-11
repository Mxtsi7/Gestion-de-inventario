using InventoryControl.UI.Views;
using Microsoft.Maui.Controls;

namespace InventoryControl.UI;

public partial class App : Microsoft.Maui.Controls.Application
{
    private readonly MainPage _mainPage;

    public App(MainPage mainPage)
    {
        InitializeComponent();
        _mainPage = mainPage;
        MainPage = new NavigationPage(_mainPage);
    }
}
