# Implementar Swagger em aplicação .NET

Olá Gustavo, abaixo irei compartilhar cada etapa implementada para o funcionamento do Swagger em seu projeto.

---

> **1ª Etapa**
>
> No arquivo *OrçamentoObra.csproj*, foi utilizado o *Swashbuckle.AspNetCore* na versão **9.0.4**. Infelizmente essa versão não suporta o Swagger corretamente, altere para a versão **5.0.0**.
>
> No seu projeto está assim: **<PackageReference Include="Swashbuckle.AspNetCore" Version="9.0.4" />**
>
> Altere para: **<PackageReference Include="Swashbuckle.AspNetCore" Version="5.0.0" />**

> **2ª Etapa**
>
> Crie uma pasta chamada *Swagger* no mesmo diretório de **Controllers**, **Data**, **Dto**, etc...
>
> Dentro da pasta **Swagger*, crie o arquivo **FileUploadOperationFilter.cs**.
>
> Agora copie este código e cole no arquivo gerado:
> using Microsoft.OpenApi.Models;
> using Swashbuckle.AspNetCore.SwaggerGen;
> using System.Collections.Generic;
> using System.Linq;
> using Microsoft.AspNetCore.Http;
> 
> namespace OrçamentoObra.Swagger
> {
>     public class FileUploadOperationFilter : IOperationFilter
>     {
>         public void Apply(OpenApiOperation operation, OperationFilterContext context)
>         {
>             // Verifica se o parâmetro existe e se é do tipo IFormFile ou List<IFormFile>
>             var fileParams = context.ApiDescription.ParameterDescriptions
>                                   .Where(p => p.ParameterDescriptor != null && 
>                                               (p.ParameterDescriptor.ParameterType == typeof(IFormFile) ||
>                                                typeof(List<IFormFile>).IsAssignableFrom(p.ParameterDescriptor.ParameterType)))
>                                   .ToList();
> 
>             if (fileParams.Any())
>             {
>                 // Se o parâmetro for IFormFile ou List<IFormFile>, configura o Swagger para usar multipart/form-data
>                 foreach (var fileParam in fileParams)
>                 {
>                     var fileParamName = fileParam.Name;
> 
>                     // Configura o corpo da requisição como multipart/form-data
>                     operation.RequestBody = new OpenApiRequestBody
>                     {
>                         Content = new Dictionary<string, OpenApiMediaType>
>                         {
>                             {
>                                 "multipart/form-data", new OpenApiMediaType
>                                 {
>                                     Schema = new OpenApiSchema
>                                     {
>                                         Type = "object",
>                                         Properties = new Dictionary<string, OpenApiSchema>
>                                         {
>                                             {
>                                                 fileParamName, new OpenApiSchema
>                                                 {
>                                                     Type = "string",
>                                                     Format = "binary" // Marca como um arquivo binário
>                                                 }
>                                             }
>                                         }
>                                     }
>                                 }
>                             }
>                         }
>                     };
>                 }
>             }
>         }
>     }
> }

> **3ª Etapa**
>
> Apague todo o código no arquivo **ItemController.cs**, em seguida copie e cole essa estrutura:
>
> using Microsoft.AspNetCore.Http;
> using Microsoft.AspNetCore.Mvc;
> using OrçamentoObra.Dto.Item;
> using OrçamentoObra.Services.Interface;
> 
> namespace OrçamentoObra.Controllers
> {
>     [Route("api/[controller]")]
>     [ApiController]
>     public class ItemController : ControllerBase
>     {
>         // Atributo da classe
>         private readonly IItemInterface _itemInterface;
> 
>         // Construtor
>         public ItemController(IItemInterface item)
>         {
>             _itemInterface = item;
>         }
> 
>         // Método para criar um item
>         [HttpPost]
>         public async Task<IActionResult> CriarItem([FromBody] ItemCreateDTO itemCreateDTO)
>         {
>             var item = await _itemInterface.CriarItem(itemCreateDTO);
>             return Ok(item);
>         }
> 
>         // Método que lista todos os itens
>         [HttpGet("")]
>         public IActionResult ListarItems()
>         {
>             // Seu código para listar todos os itens
>             return Ok();
>         }
> 
>         // Método para editar itens
>         [HttpPut("{id}")]
>         public async Task<IActionResult> EditarItem([FromBody] ItemUpdateDTO itemUpdateDTO)
>         {
>             var item = await _itemInterface.EditarItem(itemUpdateDTO);
>             return Ok(item);
>         }
> 
>         // Método para remover itens
>         [HttpDelete("{id}")]
>         public async Task<IActionResult> RemoverItem(int id)
>         {
>             var item = await _itemInterface.ExcluirItem(id);
>             return Ok(item);
>         }
> 
>         // Método para buscar um items pelo id
>         [HttpGet("{id}")]
>         public async Task<IActionResult> BuscarItemPorId(int id)
>         {
>             var item = await _itemInterface.BuscarItemPorId(id);
>             return Ok(item);
>         }
> 
>         // Método para listar itens por categoria
>         [HttpGet("categoria/{categoriaId}")]
>         public async Task<IActionResult> ListarItemPorCategoria(int categoriaId)
>         {
>             var item = await _itemInterface.ListarItemPorCategoria(categoriaId);
>             return Ok(item);
>         }
> 
>     }
> }

> **4ª Etapa**
>
> Agora você pode executar o projeto e testar. O Swagger estará em funcionamento:
> ![https://github.com/ralfslima/swagger_gustavo/blob/master/imagem_swagger.png]

---

Espero que dê tudo certo Gustavo!

Se precisar de mais alguma coisa, estou à disposição.

Bons estudos e muito sucesso ;)
