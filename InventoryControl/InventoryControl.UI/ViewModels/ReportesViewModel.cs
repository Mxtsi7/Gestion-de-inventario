using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryControl.Domain.Interfaces;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;

namespace InventoryControl.UI.ViewModels;

public partial class ReportesViewModel : ObservableObject
{
    private readonly IProductRepository _repo;

    [ObservableProperty] private DateTime fechaInicio = DateTime.Today.AddMonths(-1);
    [ObservableProperty] private DateTime fechaFin = DateTime.Today;
    [ObservableProperty] private string? categoriaSeleccionada;
    [ObservableProperty] private int totalEntradas;
    [ObservableProperty] private int totalSalidas;
    [ObservableProperty] private SparklineDrawable? sparklineEntradas;
    [ObservableProperty] private SparklineDrawable? sparklineSalidas;

    public ObservableCollection<string> Categorias { get; } = new();
    public ObservableCollection<ResumenProductoDto> ResumenProductos { get; } = new();

    public ReportesViewModel(IProductRepository repo)
    {
        _repo = repo;
        SparklineEntradas = new SparklineDrawable { Color = Color.FromArgb("#00C9A7") };
        SparklineSalidas = new SparklineDrawable { Color = Color.FromArgb("#FF6B6B") };
    }

    [RelayCommand]
    private async Task GenerarReporte()
    {
        var productos = (await _repo.GetAllAsync()).ToList();
        Categorias.Clear();
        foreach (var cat in productos.Select(p => p.Category).Distinct().OrderBy(c => c))
            Categorias.Add(cat);

        // Datos simulados hasta que exista el repositorio de movimientos
        TotalEntradas = productos.Sum(p => p.CurrentStock);
        TotalSalidas = 0;

        ResumenProductos.Clear();
        foreach (var p in productos)
        {
            ResumenProductos.Add(new ResumenProductoDto
            {
                NombreProducto = p.Name,
                TotalEntradas = p.CurrentStock,
                TotalSalidas = 0,
                SaldoNeto = p.CurrentStock
            });
        }
    }
}

public class SparklineDrawable : IDrawable
{
    public Color Color { get; set; } = Color.FromArgb("#6C3EF4");
    public List<int> Valores { get; set; } = new() { 3, 7, 5, 10, 8, 12, 9 };

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (Valores.Count < 2) return;
        float maxV = Valores.Max();
        float minV = Valores.Min();
        float range = maxV == minV ? 1 : maxV - minV;
        float stepX = dirtyRect.Width / (Valores.Count - 1);

        var path = new PathF();
        for (int i = 0; i < Valores.Count; i++)
        {
            float x = i * stepX;
            float y = dirtyRect.Height - ((Valores[i] - minV) / range) * (dirtyRect.Height - 8) - 4;
            if (i == 0) path.MoveTo(x, y);
            else path.LineTo(x, y);
        }
        canvas.StrokeColor = Color;
        canvas.StrokeSize = 2;
        canvas.DrawPath(path);
    }
}

public class ResumenProductoDto
{
    public string NombreProducto { get; set; } = string.Empty;
    public int TotalEntradas { get; set; }
    public int TotalSalidas { get; set; }
    public int SaldoNeto { get; set; }
    public Color SaldoColor => SaldoNeto >= 0 ? Color.FromArgb("#0a8a6e") : Color.FromArgb("#c94040");
}