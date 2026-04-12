using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryControl.UI.Popups;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;

namespace InventoryControl.UI.ViewModels;

public partial class MovimientosViewModel : ObservableObject
{
    private readonly RegistrarMovimientoViewModel _popupVm;

    public ObservableCollection<MovimientoFilaDto> Movimientos { get; } = new();

    public MovimientosViewModel(RegistrarMovimientoViewModel popupVm)
    {
        _popupVm = popupVm;
    }

    [RelayCommand]
    private Task CargarMovimientos()
    {
        // Aquí se conectará al repositorio de movimientos cuando exista
        Movimientos.Clear();
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task AbrirModal()
    {
        var popup = new RegistrarMovimientoPopup(_popupVm);
        await Application.Current!.MainPage!.ShowPopupAsync(popup);
        await CargarMovimientos();
    }
}

public class MovimientoFilaDto
{
    public string NombreProducto { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public string Motivo { get; set; } = string.Empty;
    public string FechaCorta { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public int Indice { get; set; }

    public Color FilaColor => Indice % 2 == 0 ? Color.FromArgb("#FFFFFF") : Color.FromArgb("#F8F7FF");
    public Color TipoBadgeBg => Tipo == "Entrada" ? Color.FromArgb("#E0FBF5") : Color.FromArgb("#fff0ef");
    public Color TipoBadgeFg => Tipo == "Entrada" ? Color.FromArgb("#0a8a6e") : Color.FromArgb("#c94040");
}