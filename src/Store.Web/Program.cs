using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Store.Infrastructure;

namespace Store.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var webHost = CreateHostBuilder(args).Build();

        await ApplyMigrations(webHost.Services);

        await webHost.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

    private static async Task ApplyMigrations(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        // dotnet ef migrations -p ../Store.Infrastructure add "migration_name" -c ApplicationDbContext
        // dotnet ef database update -p ../Store.Infrastructure -c ApplicationDbContext

        await dbContext.Database.MigrateAsync();
    }
}