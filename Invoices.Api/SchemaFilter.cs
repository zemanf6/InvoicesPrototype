using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Invoices.Api
{
    public class DateOnlySchemaFilter : ISchemaFilter
    {
        // Aby to swagger ukazoval správně, není nutné
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(DateOnly))
            {
                schema.Type = "string";
                schema.Format = "date";
                schema.Example = new Microsoft.OpenApi.Any.OpenApiString("2024-01-01");
            }
        }
    }
}
