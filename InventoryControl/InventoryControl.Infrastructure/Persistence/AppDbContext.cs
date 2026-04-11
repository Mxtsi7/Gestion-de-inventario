using InventoryControl.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryControl.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}