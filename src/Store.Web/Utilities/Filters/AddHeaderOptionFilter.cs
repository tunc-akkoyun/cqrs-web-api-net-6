using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Store.Web.Utilities.Filters;

public class AddHeaderOperationFilter : IOperationFilter
{
    private readonly string _parameterName;
    private readonly string _description;
    private readonly string _format;
    private readonly bool _required;

    public AddHeaderOperationFilter(string parameterName, string description, string format, bool required = false)
    {
        _parameterName = parameterName;
        _description = description;
        _format = format;
        _required = required;
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = _parameterName,
            In = ParameterLocation.Header,
            Description = _description,
            Required = _required,
            Schema = new()
            {
                Type = "string",
                Format = _format
            }
        });
    }
}