using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Domain.Abstractions;
using Store.Infrastructure.Persistence;
using Store.Infrastructure.Repositories;

namespace Store.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(builder =>
           builder.UseSqlServer(configuration.GetConnectionString("Application")));

        services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

        return services;
    }
}