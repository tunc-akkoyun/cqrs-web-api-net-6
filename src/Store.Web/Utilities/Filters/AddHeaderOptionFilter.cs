using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Store.Web.Utilities.Filters;

public class AddHeaderOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "correlation_id",
            In = ParameterLocation.Header,
            Description = "Correlation identifier for the request",
            Required = true,
            Schema = new()
            {
                Type = "string",
                Format = "uuid"
            }
        });

        if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor descriptor)
        {
            // If [AllowAnonymous] is not applied or [Authorize] or Custom Authorization filter is applied on either the endpoint or the controller
            if (!context.ApiDescription.CustomAttributes().Any((a) => a is AllowAnonymousAttribute)
                && (context.ApiDescription.CustomAttributes().Any((a) => a is AuthorizeAttribute)
                    || descriptor.ControllerTypeInfo.GetCustomAttribute<AuthorizeAttribute>() != null))
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "authorization",
                    In = ParameterLocation.Header,
                    Description = "Bearer access token",
                    Required = true,
                    Schema = new()
                    {
                        Type = "string"
                    }
                });
            }
        }
    }
}