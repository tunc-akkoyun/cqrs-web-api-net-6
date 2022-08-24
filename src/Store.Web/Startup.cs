using System;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Store.Application.Behaviors;
using Store.Domain.Abstractions;
using Store.Infrastructure;
using Store.Infrastructure.Repositories;
using Store.Web.Middleware;
using Store.Web.Utilities.Filters;

namespace Store.Web;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var presentationAssembly = typeof(AssemblyReference).Assembly;

        services.AddControllers()
            .AddApplicationPart(presentationAssembly);

        var applicationAssembly = typeof(Application.AssemblyReference).Assembly;

        services.AddMediatR(applicationAssembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(applicationAssembly);

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

            c.OperationFilter<AddHeaderOperationFilter>("authorization", "Bearer access token", "");
            c.OperationFilter<AddHeaderOperationFilter>("correlation_id", "Correlation identifier for the request", "uuid");
            c.SchemaFilter<SwaggerJsonIgnoreFilter>();
        });


        services.AddDbContext<ApplicationDbContext>(builder =>
            builder.UseSqlServer(Configuration.GetConnectionString("Application")));

        services.AddScoped<IUnitOfWork, EFUnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(EFRepository<>));

        services.AddTransient<ExceptionHandlingMiddleware>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = false,
                DefaultContentType = "text/plain"
            });

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "docs/{documentName}/swagger.json";
            });

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

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}