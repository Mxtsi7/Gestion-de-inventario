using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace InventoryControl.UI.ViewModels;

public partial class MovementsViewModel : ObservableObject
{
    public ObservableCollection<MovementRecord> Movements { get; } = new()
    {
        new MovementRecord { ProductName = "Laptop HP ProBook 450", Sku = "LP-001", Type = "Entrada", TypeColor = Colors.MediumSeaGreen, Quantity = 15, Date = new DateTime(2026,4,12,14,30,0), Reason = "Compra", UserName = "Juan Pérez" },
        new MovementRecord { ProductName = "Mouse Logitech MX Master 3", Sku = "MS-024", Type = "Salida", TypeColor = Colors.Tomato, Quantity = 8, Date = new DateTime(2026,4,12,11,15,0), Reason = "Venta", UserName = "María García" },
        new MovementRecord { ProductName = "Teclado Mecánico Keychron", Sku = "KB-015", Type = "Entrada", TypeColor = Colors.MediumSeaGreen, Quantity = 20, Date = new DateTime(2026,4,12,9,45,0), Reason = "Compra", UserName = "Carlos López" },
        new MovementRecord { ProductName = "Monitor Dell 27\" UltraSharp", Sku = "MN-008", Type = "Salida", TypeColor = Colors.Tomato, Quantity = 5, Date = new DateTime(2026,4,11,16,20,0), Reason = "Venta", UserName = "Ana Martínez" },
    };

    [RelayCommand]
    private async Task RegisterMovementAsync()
    {
        await Shell.Current.GoToAsync("movementform");
    }
}

public class MovementRecord
{
    public string ProductName { get; set; } = "";
    public string Sku { get; set; } = "";
    public string Type { get; set; } = "";
    public Color TypeColor { get; set; } = Colors.Gray;
    public int Quantity { get; set; }
    public DateTime Date { get; set; }
    public string Reason { get; set; } = "";
    public string UserName { get; set; } = "";
}