using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace InventoryControl.UI.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    [ObservableProperty] private int totalProducts = 0;
    [ObservableProperty] private int inStock = 0;
    [ObservableProperty] private int lowStock = 0;
    [ObservableProperty] private int movementsToday = 0;
    [ObservableProperty] private bool isLoading = false;

    public ObservableCollection<StockAlertItem> StockAlerts { get; } = new();
    public ObservableCollection<MovementItem> RecentMovements { get; } = new();

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        if (IsLoading) return;
        IsLoading = true;

        try
        {
            // TODO: Reemplazar con datos reales desde MediatR
            await Task.Delay(100);

            TotalProducts = 248;
            InStock = 219;
            LowStock = 18;
            MovementsToday = 34;

            StockAlerts.Clear();
            StockAlerts.Add(new StockAlertItem { Name = "Laptop Dell XPS 15", CurrentStock = 3 });
            StockAlerts.Add(new StockAlertItem { Name = "Mouse Logitech MX", CurrentStock = 5 });
            StockAlerts.Add(new StockAlertItem { Name = "Teclado Mecánico", CurrentStock = 2 });
            StockAlerts.Add(new StockAlertItem { Name = "Monitor 27\" LG", CurrentStock = 4 });

            RecentMovements.Clear();
            RecentMovements.Add(new MovementItem { ProductName = "Laptop HP ProBook", Sku = "LP-001", Type = "Entrada", Quantity = 15, TypeColor = Colors.MediumSeaGreen });
            RecentMovements.Add(new MovementItem { ProductName = "Mouse inalámbrico", Sku = "MS-024", Type = "Salida", Quantity = 8, TypeColor = Colors.Tomato });
            RecentMovements.Add(new MovementItem { ProductName = "Teclado USB", Sku = "KB-015", Type = "Entrada", Quantity = 20, TypeColor = Colors.MediumSeaGreen });
        }
        finally
        {
            IsLoading = false;
        }
    }
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
