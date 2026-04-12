using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryControl.Application.DTOs;
using InventoryControl.Domain.Interfaces;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;

namespace InventoryControl.UI.ViewModels;

public partial class DashboardViewModel : ObservableObject
{
    private readonly IProductRepository _productRepo;

    [ObservableProperty] private int totalProductos;
    [ObservableProperty] private int productosEnStock;
    [ObservableProperty] private int productosStockBajo;
    [ObservableProperty] private int movimientosHoy;
    [ObservableProperty] private GraficoBarrasDrawable? graficoSemanal;

    public ObservableCollection<AlertaStockDto> AlertasStock { get; } = new();
    public ObservableCollection<MovimientoResumenDto> UltimosMovimientos { get; } = new();

    public DashboardViewModel(IProductRepository productRepo)
    {
        _productRepo = productRepo;
        GraficoSemanal = new GraficoBarrasDrawable();
    }

    [RelayCommand]
    private async Task CargarDashboardAsync()
    {
        var productos = (await _productRepo.GetAllAsync()).ToList();
        TotalProductos = productos.Count;
        ProductosEnStock = productos.Count(p => p.CurrentStock > p.MinimumStockThreshold);
        ProductosStockBajo = productos.Count(p => p.CurrentStock > 0 && p.CurrentStock <= p.MinimumStockThreshold);
        MovimientosHoy = 0; // requiere repositorio de movimientos

        AlertasStock.Clear();
        foreach (var p in productos.Where(p => p.CurrentStock <= p.MinimumStockThreshold).Take(5))
        {
            AlertasStock.Add(new AlertaStockDto
            {
                NombreProducto = p.Name,
                CantidadActual = p.CurrentStock,
                PorcentajeStock = p.MinimumStockThreshold == 0 ? 0
                    : Math.Min(1f, (float)p.CurrentStock / p.MinimumStockThreshold)
            });
        }
    }
}

public class GraficoBarrasDrawable : IDrawable
{
    public List<int> Valores { get; set; } = new() { 12, 8, 15, 6, 20, 5, 10 };

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        int count = Valores.Count;
        float maxVal = Valores.Max();
        float barWidth = dirtyRect.Width / (count * 1.5f);
        float gap = barWidth * 0.5f;
        float baseY = dirtyRect.Height - 4;
        Color[] colors = { Color.FromArgb("#6C3EF4"), Color.FromArgb("#00C9A7") };

        for (int i = 0; i < count; i++)
        {
            float x = i * (barWidth + gap) + gap;
            float barH = maxVal == 0 ? 0 : (Valores[i] / maxVal) * (dirtyRect.Height - 20);
            canvas.FillColor = colors[i % 2];
            canvas.FillRoundedRectangle(x, baseY - barH, barWidth, barH, 4);
        }
    }
}

public class AlertaStockDto
{
    public string NombreProducto { get; set; } = string.Empty;
    public int CantidadActual { get; set; }
    public float PorcentajeStock { get; set; }
}

public class MovimientoResumenDto
{
    public string NombreProducto { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public string FechaCorta { get; set; } = string.Empty;
    public Color TipoBadgeBg => Tipo == "Entrada" ? Color.FromArgb("#E0FBF5") : Color.FromArgb("#fff0ef");
    public Color TipoBadgeFg => Tipo == "Entrada" ? Color.FromArgb("#0a8a6e") : Color.FromArgb("#c94040");
}