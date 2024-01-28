using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VirtualTeacher.Helpers
{
    public class SwaggerTagsFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var tagsAttribute = (TagsAttribute)context.ApiDescription.ActionDescriptor.EndpointMetadata
                .FirstOrDefault(em => em.GetType() == typeof(TagsAttribute));
            // Define your tags based on your application logic
            if (tagsAttribute != null)
            {
                operation.Tags = new List<OpenApiTag>
                {
                    new OpenApiTag { Name = tagsAttribute.Tags[0] } // Assuming you have one tag per method
                };
            }

 
        }
    }
}
