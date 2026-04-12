using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace InventoryControl.UI.ViewModels;

public partial class ReportsViewModel : ObservableObject
{
    [ObservableProperty] private int monthlyMovements = 890;
    [ObservableProperty] private string monthlyMovementsChange = "↑ +12.5% vs mes anterior";
    [ObservableProperty] private decimal totalValue = 185000;
    [ObservableProperty] private string totalValueChange = "↑ +8.2% vs mes anterior";
    [ObservableProperty] private int avgRotation = 24;
    [ObservableProperty] private string rotationChange = "↓ -3 días vs mes anterior";

    public ObservableCollection<TopProductItem> TopProducts { get; } = new()
    {
        new TopProductItem { Rank = "1", Name = "Cable USB-C 2m", Movements = 156, Change = "+12%" },
        new TopProductItem { Rank = "2", Name = "Laptop HP ProBook 450", Movements = 89, Change = "+8%" },
        new TopProductItem { Rank = "3", Name = "Mouse Logitech MX Master 3", Movements = 67, Change = "+15%" },
        new TopProductItem { Rank = "4", Name = "SSD Samsung 1TB NVMe", Movements = 54, Change = "+5%" },
        new TopProductItem { Rank = "5", Name = "Auriculares Sony WH-1000XM5", Movements = 43, Change = "+22%" },
    };
}

public class TopProductItem
{
    public string Rank { get; set; } = "";
    public string Name { get; set; } = "";
    public int Movements { get; set; }
    public string Change { get; set; } = "";
}