using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryControl.Domain.Interfaces;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Collections.ObjectModel;

namespace InventoryControl.UI.ViewModels;

public partial class ProductosViewModel : ObservableObject
{
    private readonly IProductRepository _repo;
    private List&lt;ProductoFilaDto&gt; _todos = new();
    private const int ItemsPorPagina = 10;

    [ObservableProperty] private string textoBusqueda = string.Empty;
    [ObservableProperty] private string? categoriaSeleccionada;
    [ObservableProperty] private string? estadoSeleccionado;
    [ObservableProperty] private int paginaActual = 1;
    [ObservableProperty] private int totalPaginas = 1;

    public ObservableCollection&lt;string&gt; Categorias { get; } = new();
    public ObservableCollection&lt;ProductoFilaDto&gt; ProductosFiltrados { get; } = new();

    public ProductosViewModel(IProductRepository repo)
    {
        _repo = repo;
    }

    partial void OnTextoBusquedaChanged(string value) =&gt; AplicarFiltros();
    partial void OnCategoriaSeleccionadaChanged(string? value) =&gt; AplicarFiltros();
    partial void OnEstadoSeleccionadoChanged(string? value) =&gt; AplicarFiltros();

    [RelayCommand]
    private async Task CargarProductos()
    {
        var productos = (await _repo.GetAllAsync()).ToList();
        _todos = productos.Select((p, i) =&gt; new ProductoFilaDto
        {
            Id = p.Id,
            Nombre = p.Name,
            SKU = p.Sku,
            Categoria = p.Category,
            StockActual = p.CurrentStock,
            StockMinimo = p.MinimumStockThreshold,
            Precio = p.UnitPrice,
            Estado = p.GetStockLevelStatus() switch
            {
                "OK"  =&gt; "Disponible",
                "LOW" =&gt; "Stock bajo",
                _     =&gt; "Agotado"
            },
            Indice = i
        }).ToList();

        Categorias.Clear();
        foreach (var cat in _todos.Select(p =&gt; p.Categoria).Distinct().OrderBy(c =&gt; c))
            Categorias.Add(cat);

        AplicarFiltros();
    }

    private void AplicarFiltros()
    {
        var filtrado = _todos.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(TextoBusqueda))
            filtrado = filtrado.Where(p =&gt;
                p.Nombre.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase) ||
                p.SKU.Contains(TextoBusqueda, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrEmpty(CategoriaSeleccionada))
            filtrado = filtrado.Where(p =&gt; p.Categoria == CategoriaSeleccionada);

        if (!string.IsNullOrEmpty(EstadoSeleccionado) &amp;&amp; EstadoSeleccionado != "Todos")
            filtrado = filtrado.Where(p =&gt; p.Estado == EstadoSeleccionado);

        var lista = filtrado.ToList();
        TotalPaginas = Math.Max(1, (int)Math.Ceiling(lista.Count / (double)ItemsPorPagina));
        PaginaActual = Math.Min(PaginaActual, TotalPaginas);

        var pagina = lista
            .Skip((PaginaActual - 1) * ItemsPorPagina)
            .Take(ItemsPorPagina)
            .ToList();

        ProductosFiltrados.Clear();
        foreach (var p in pagina) ProductosFiltrados.Add(p);
    }

    [RelayCommand]
    private void PaginaAnterior()
    {
        if (PaginaActual &gt; 1) { PaginaActual--; AplicarFiltros(); }
    }

    [RelayCommand]
    private void PaginaSiguiente()
    {
        if (PaginaActual &lt; TotalPaginas) { PaginaActual++; AplicarFiltros(); }
    }

    [RelayCommand]
    private async Task AgregarProducto()
    {
        await Shell.Current.GoToAsync("productForm");
    }

    [RelayCommand]
    private async Task EditarProducto(ProductoFilaDto? dto)
    {
        if (dto is null) return;
        await Shell.Current.GoToAsync("productForm", new Dictionary&lt;string, object&gt;
        {
            { "id", dto.Id.ToString() }
        });
    }

    [RelayCommand]
    private async Task EliminarProducto(Guid id)
    {
        var mainPage = Microsoft.Maui.Controls.Application.Current?.MainPage;
        if (mainPage is null) return;

        bool ok = await mainPage.DisplayAlert(
            "Eliminar", "¿Eliminar este producto?", "Sí", "No");
        if (!ok) return;

        await _repo.DeleteAsync(id);
        await CargarProductos();
    }
}

public class ProductoFilaDto
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public int StockActual { get; set; }
    public int StockMinimo { get; set; }
    public decimal Precio { get; set; }
    public string Estado { get; set; } = string.Empty;
    public int Indice { get; set; }

    public Color FilaColor =&gt; Indice % 2 == 0
        ? Color.FromArgb("#FFFFFF")
        : Color.FromArgb("#F8F7FF");

    public Color StockColor =&gt; Estado == "Disponible"
        ? Color.FromArgb("#0a8a6e")
        : Estado == "Stock bajo"
            ? Color.FromArgb("#c47a00")
            : Color.FromArgb("#c94040");

    public Color EstadoBadgeBg =&gt; Estado == "Disponible"
        ? Color.FromArgb("#e0fbf5")
        : Estado == "Stock bajo"
            ? Color.FromArgb("#fff4e0")
            : Color.FromArgb("#fff0ef");

    public Color EstadoBadgeFg =&gt; Estado == "Disponible"
        ? Color.FromArgb("#0a8a6e")
        : Estado == "Stock bajo"
            ? Color.FromArgb("#c47a00")
            : Color.FromArgb("#c94040");
}
