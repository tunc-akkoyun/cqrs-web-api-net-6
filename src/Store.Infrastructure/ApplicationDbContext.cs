using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;

namespace Store.Infrastructure;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Parameter> Parameters { get; set; }
    public DbSet<Release> Releases { get; set; }

    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
}