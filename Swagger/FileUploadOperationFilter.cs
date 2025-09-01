using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace OrçamentoObra.Swagger
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Verifica se o parâmetro existe e se é do tipo IFormFile ou List<IFormFile>
            var fileParams = context.ApiDescription.ParameterDescriptions
                                  .Where(p => p.ParameterDescriptor != null && 
                                              (p.ParameterDescriptor.ParameterType == typeof(IFormFile) ||
                                               typeof(List<IFormFile>).IsAssignableFrom(p.ParameterDescriptor.ParameterType)))
                                  .ToList();

            if (fileParams.Any())
            {
                // Se o parâmetro for IFormFile ou List<IFormFile>, configura o Swagger para usar multipart/form-data
                foreach (var fileParam in fileParams)
                {
                    var fileParamName = fileParam.Name;

                    // Configura o corpo da requisição como multipart/form-data
                    operation.RequestBody = new OpenApiRequestBody
                    {
                        Content = new Dictionary<string, OpenApiMediaType>
                        {
                            {
                                "multipart/form-data", new OpenApiMediaType
                                {
                                    Schema = new OpenApiSchema
                                    {
                                        Type = "object",
                                        Properties = new Dictionary<string, OpenApiSchema>
                                        {
                                            {
                                                fileParamName, new OpenApiSchema
                                                {
                                                    Type = "string",
                                                    Format = "binary" // Marca como um arquivo binário
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    };
                }
            }
        }
    }
}
