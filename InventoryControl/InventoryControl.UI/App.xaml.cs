using InventoryControl.UI.Views;

namespace InventoryControl.UI;

public partial class App : Application
{
    private readonly MainPage _mainPage;

    public App(MainPage mainPage)
    {
        InitializeComponent();
        _mainPage = mainPage;
        MainPage = new NavigationPage(_mainPage);
    }
}
