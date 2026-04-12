using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryControl.Application.DTOs;
using InventoryControl.Domain.Interfaces;
using System.Collections.ObjectModel;

namespace InventoryControl.UI.ViewModels;

public partial class RegistrarMovimientoViewModel : ObservableObject
{
    private readonly IProductRepository _repo;

    [ObservableProperty] private bool entradaActiva = true;
    [ObservableProperty] private bool salidaActiva = false;
    [ObservableProperty] private ProductDto? productoSeleccionado;
    [ObservableProperty] private int cantidad = 1;
    [ObservableProperty] private DateTime fechaMovimiento = DateTime.Today;
    [ObservableProperty] private string? motivoSeleccionado;
    [ObservableProperty] private string notas = string.Empty;

    public string TipoSeleccionado => EntradaActiva ? "Entrada" : "Salida";
    public ObservableCollection<ProductDto> Productos { get; } = new();
    public Action? CerrarAction { get; set; }

    public RegistrarMovimientoViewModel(IProductRepository repo)
    {
        _repo = repo;
        _ = CargarProductosAsync();
    }

    private async Task CargarProductosAsync()
    {
        var lista = await _repo.GetAllAsync();
        Productos.Clear();
        foreach (var p in lista)
            Productos.Add(new ProductDto(p.Id, p.Name, p.Sku, p.Category,
                p.UnitPrice, p.MinimumStockThreshold, p.CurrentStock, p.GetStockLevelStatus()));
    }

    [RelayCommand]
    private void SeleccionarEntrada() { EntradaActiva = true; SalidaActiva = false; }

    [RelayCommand]
    private void SeleccionarSalida() { EntradaActiva = false; SalidaActiva = true; }

    [RelayCommand]
    private void Incrementar() => Cantidad++;

    [RelayCommand]
    private void Decrementar() { if (Cantidad > 1) Cantidad--; }

    [RelayCommand]
    private async Task Confirmar()
    {
        if (ProductoSeleccionado is null || Cantidad < 1) return;

        var producto = await _repo.GetByIdAsync(ProductoSeleccionado.Id);
        if (producto is null) return;

        if (EntradaActiva)
            producto.AddStock(Cantidad);
        else
            producto.RemoveStock(Cantidad);

        await _repo.UpdateAsync(producto);
        CerrarAction?.Invoke();
    }

    [RelayCommand]
    private void Cancelar() => CerrarAction?.Invoke();

    [RelayCommand]
    private void Cerrar() => CerrarAction?.Invoke();
}