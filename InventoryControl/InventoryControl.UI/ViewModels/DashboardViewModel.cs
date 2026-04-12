using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace InventoryControl.UI.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    [ObservableProperty] private int totalProducts = 248;
    [ObservableProperty] private int inStock = 219;
    [ObservableProperty] private int lowStock = 18;
    [ObservableProperty] private int movementsToday = 34;

    public ObservableCollection<StockAlertItem> StockAlerts { get; } = new()
    {
        new StockAlertItem { Name = "Laptop Dell XPS 15", CurrentStock = 3 },
        new StockAlertItem { Name = "Mouse Logitech MX", CurrentStock = 5 },
        new StockAlertItem { Name = "Teclado Mecánico", CurrentStock = 2 },
        new StockAlertItem { Name = "Monitor 27\" LG", CurrentStock = 4 },
    };

    public ObservableCollection<MovementItem> RecentMovements { get; } = new()
    {
        new MovementItem { ProductName = "Laptop HP ProBook", Sku = "LP-001", Type = "Entrada", Quantity = 15, TypeColor = Colors.MediumSeaGreen },
        new MovementItem { ProductName = "Mouse inalámbrico", Sku = "MS-024", Type = "Salida", Quantity = 8, TypeColor = Colors.Tomato },
        new MovementItem { ProductName = "Teclado USB", Sku = "KB-015", Type = "Entrada", Quantity = 20, TypeColor = Colors.MediumSeaGreen },
    };
}

public class StockAlertItem
{
    public string Name { get; set; } = "";
    public int CurrentStock { get; set; }
}

public class MovementItem
{
    public string ProductName { get; set; } = "";
    public string Sku { get; set; } = "";
    public string Type { get; set; } = "";
    public int Quantity { get; set; }
    public Color TypeColor { get; set; } = Colors.Gray;
}