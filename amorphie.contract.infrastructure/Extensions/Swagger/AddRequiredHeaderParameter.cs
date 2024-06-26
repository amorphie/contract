using amorphie.contract.core.Enum;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace amorphie.contract.infrastructure.Extensions
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.RelativePath.StartsWith("admin/"))
            {
                context.ApiDescription.GroupName = "amorphie.contract.admin";
            }
            else
            {
                context.ApiDescription.GroupName = "amorphie.contract.v1";
            }

            if (operation.Parameters is null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = AppHeaderConsts.BusinessLine,
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

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = AppHeaderConsts.UserReference,
                In = ParameterLocation.Header,
                Required = false,
                Description = "Indicates user reference."
            });

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = AppHeaderConsts.CustomerNo,
                In = ParameterLocation.Header,
                Required = false,
                Description = "Indicates customer no."
            });
        }
    }

}