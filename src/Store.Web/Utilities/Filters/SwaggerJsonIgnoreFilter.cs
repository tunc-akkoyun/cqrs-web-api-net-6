using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Store.Web.Utilities.Filters;

public class SwaggerJsonIgnoreFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var type = context.Type;
        if (!schema.Properties.Any() || type == null) return;

        var excludedPropertyNames = type
                .GetProperties()
                .Where(
                    t => t.GetCustomAttribute<JsonIgnoreAttribute>() != null
                ).Select(d => d.Name).ToList();

        if (!excludedPropertyNames.Any()) return;

        foreach (var excludedProperty in excludedPropertyNames)
        {
            var key = char.ToLower(excludedProperty[0]) + excludedProperty.Substring(1);
            if (schema.Properties.ContainsKey(key))
                schema.Properties.Remove(key);
        }
    }
}