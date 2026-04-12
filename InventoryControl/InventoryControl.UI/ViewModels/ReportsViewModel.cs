using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace InventoryControl.UI.ViewModels;

public partial class ReportsViewModel : ObservableObject
{
    [ObservableProperty] private int monthlyMovements = 0;
    [ObservableProperty] private string monthlyMovementsChange = "";
    [ObservableProperty] private decimal totalValue = 0;
    [ObservableProperty] private string totalValueChange = "";
    [ObservableProperty] private int avgRotation = 0;
    [ObservableProperty] private string rotationChange = "";
    [ObservableProperty] private bool isLoading = false;

    public ObservableCollection<TopProductItem> TopProducts { get; } = new();

    [RelayCommand]
    private async Task LoadReportsAsync()
    {
        if (IsLoading) return;
        IsLoading = true;

        try
        {
            // TODO: Reemplazar con datos reales desde MediatR
            await Task.Delay(100);

            MonthlyMovements = 890;
            MonthlyMovementsChange = "↑ +12.5% vs mes anterior";
            TotalValue = 185000;
            TotalValueChange = "↑ +8.2% vs mes anterior";
            AvgRotation = 24;
            RotationChange = "↓ -3 días vs mes anterior";

            TopProducts.Clear();
            TopProducts.Add(new TopProductItem { Rank = "1", Name = "Cable USB-C 2m", Movements = 156, Change = "+12%" });
            TopProducts.Add(new TopProductItem { Rank = "2", Name = "Laptop HP ProBook 450", Movements = 89, Change = "+8%" });
            TopProducts.Add(new TopProductItem { Rank = "3", Name = "Mouse Logitech MX Master 3", Movements = 67, Change = "+15%" });
            TopProducts.Add(new TopProductItem { Rank = "4", Name = "SSD Samsung 1TB NVMe", Movements = 54, Change = "+5%" });
            TopProducts.Add(new TopProductItem { Rank = "5", Name = "Auriculares Sony WH-1000XM5", Movements = 43, Change = "+22%" });
        }
        finally
        {
            IsLoading = false;
        }
    }
}

public class TopProductItem
{
    public string Rank { get; set; } = "";
    public string Name { get; set; } = "";
    public int Movements { get; set; }
    public string Change { get; set; } = "";
}
