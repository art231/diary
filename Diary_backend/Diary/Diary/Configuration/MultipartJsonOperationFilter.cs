using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Diary.Api.Configuration
{
    public sealed class MultiPartJsonOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var descriptors = context.ApiDescription.ActionDescriptor.Parameters.ToList();
            foreach (var descriptor in descriptors.Where(x => x.BindingInfo.BinderType == typeof(JsonFormDataModelBinder)))
            {
                var mediaType = operation.RequestBody.Content.First().Value;
                mediaType.Schema.Properties.Add(descriptor.BindingInfo.BinderModelName!, operation.Parameters![0].Schema);
                operation.Parameters = null;
            }
        }
    }
}
