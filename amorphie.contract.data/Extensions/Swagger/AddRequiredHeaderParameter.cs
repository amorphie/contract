using amorphie.contract.core.Enum;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace amorphie.contract.data.Extensions
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters is null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = AppHeaderConsts.Application,
                In = ParameterLocation.Header,
                Required = false,
                Description = "Indicates Mobile ON or Mobile Burgan separation."
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = AppHeaderConsts.ClientId,
                In = ParameterLocation.Header,
                Required = false,
                Description = "Indicates client name."
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = AppHeaderConsts.AcceptLanguage,
                In = ParameterLocation.Header,
                Required = false,
                Description = "Indicates client language code."
            });
        }
    }

}