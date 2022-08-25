using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Store.Application;
using Store.Infrastructure;
using Store.Infrastructure.Persistence;
using Store.Presentation;
using Store.Web.Middleware;
using Store.Web.Utilities.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add project class libraries
var services = builder.Services;
services.AddApplication();
services.AddInfrastructure(builder.Configuration);
services.AddPresentation(builder.Configuration);
services.AddTransient<ExceptionHandlingMiddleware>();

// Apply swagger doc
ApplySwaggerRedoc(services);

var app = builder.Build();

// Apply any pending migrations
await ApplyMigrations(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger(c => { c.RouteTemplate = "docs/{documentName}/swagger.json"; });
    app.UseReDoc(c =>
    {
        c.DocumentTitle = "Store API";
        c.RoutePrefix = "docs";
        c.SpecUrl("v1/swagger.json");
        c.EnableUntrustedSpec();
        c.ScrollYOffset(10);
        c.HideDownloadButton();
        c.ExpandResponses("200,201,204");
        c.NoAutoAuth();
        c.PathInMiddlePanel();
        c.HideLoading();
        c.NativeScrollbars();
        c.OnlyRequiredInSamples();
    });
}

app.UseStaticFiles(new StaticFileOptions
{
    ServeUnknownFileTypes = false,
    DefaultContentType = "text/plain"
});


app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

// Run the web app
await app.RunAsync();


static async Task ApplyMigrations(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();

    await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // dotnet ef migrations -p ../Store.Infrastructure add "migration_name" -c ApplicationDbContext
    // dotnet ef database update -p ../Store.Infrastructure -c ApplicationDbContext

    await dbContext.Database.MigrateAsync();
}

static void ApplySwaggerRedoc(IServiceCollection services)
{
    services.AddSwaggerGenNewtonsoftSupport();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Store API",
            Version = $"{1}.{0}", //Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion,
            Description = "# Introduction\n" +
            "## About the API\n" +
            "About the API."
        });

        c.TagActionsBy(api =>
        {
            if (api.GroupName != "Base")
                return new[] { api.GroupName };

            var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
                return new[] { controllerActionDescriptor.ControllerName };

            throw new InvalidOperationException("Unable to determine tag for endpoint.");
        });

        c.DocInclusionPredicate((name, api) => true);
        c.EnableAnnotations();
        c.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName));
        c.DescribeAllParametersInCamelCase();

        c.OperationFilter<AddHeaderOperationFilter>();
        c.SchemaFilter<SwaggerJsonIgnoreFilter>();
    });
}