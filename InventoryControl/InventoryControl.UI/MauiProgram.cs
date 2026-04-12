using CommunityToolkit.Maui;
using FluentValidation;
using InventoryControl.Application.Commands.RegisterProduct;
using InventoryControl.Application.Validators;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Infrastructure.Persistence;
using InventoryControl.UI.ViewModels;
using InventoryControl.UI.Views;
using InventoryControl.UI.Popups;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace InventoryControl.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // EF Core SQLite
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "inventory.db");
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        // Repositorios
        builder.Services.AddScoped<IProductRepository, ProductRepository>();

        // MediatR
        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(RegisterProductCommand).Assembly));

        // FluentValidation
        builder.Services.AddValidatorsFromAssemblyContaining<RegisterProductCommandValidator>();

        // ─── ViewModels ───
        builder.Services.AddTransient<DashboardViewModel>();
        builder.Services.AddTransient<ProductosViewModel>();
        builder.Services.AddTransient<MovimientosViewModel>();
        builder.Services.AddTransient<RegistrarMovimientoViewModel>();
        builder.Services.AddTransient<ReportesViewModel>();
        // legacy
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<ProductFormViewModel>();

        // ─── Views ───
        builder.Services.AddTransient<DashboardPage>();
        builder.Services.AddTransient<ProductosPage>();
        builder.Services.AddTransient<MovimientosPage>();
        builder.Services.AddTransient<ReportesPage>();
        builder.Services.AddTransient<RegistrarMovimientoPopup>();
        // legacy
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<ProductFormPage>();

        var app = builder.Build();

        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();

        return app;
    }
}