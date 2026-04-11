using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Microsoft.EntityFrameworkCore;
using MediatR;
using FluentValidation;
using InventoryControl.Application.Commands.RegisterProduct;
using InventoryControl.Application.Validators;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Infrastructure.Persistence;
using InventoryControl.UI.ViewModels;
using InventoryControl.UI.Views;

namespace InventoryControl.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // EF Core SQLite
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "inventory.db");
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        // Repositories
        builder.Services.AddScoped<IProductRepository, ProductRepository>();

        // MediatR
        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(RegisterProductCommand).Assembly));

        // FluentValidation
        builder.Services.AddValidatorsFromAssemblyContaining<RegisterProductCommandValidator>();

        // ViewModels & Views
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<ProductFormViewModel>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<ProductFormPage>();

        var app = builder.Build();

        // Auto-migrate DB on startup
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();

        return app;
    }
}
