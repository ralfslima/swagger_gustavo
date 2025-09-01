# Implementando Swagger em aplicação .NET

Olá Gustavo 👋  
Aqui estão as etapas que segui para configurar o Swagger no seu projeto **OrçamentoObra**.

---

## 1. Ajuste da versão do Swashbuckle

No arquivo **OrçamentoObra.csproj**, a versão do pacote utilizada (**9.0.4**) não possui suporte adequado.  
Altere para a versão **5.0.0**:

```xml
<!-- Versão incorreta -->
<PackageReference Include="Swashbuckle.AspNetCore" Version="9.0.4" />

<!-- Versão correta -->
<PackageReference Include="Swashbuckle.AspNetCore" Version="5.0.0" />
```

---

## 2. Criando filtro para upload de arquivos

Crie uma pasta chamada **Swagger** no mesmo diretório de *Controllers*, *Data*, *Dto*, etc.  
Dentro dela, adicione o arquivo **FileUploadOperationFilter.cs** com o seguinte conteúdo:

```csharp
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
            var fileParams = context.ApiDescription.ParameterDescriptions
                .Where(p => p.ParameterDescriptor != null &&
                            (p.ParameterDescriptor.ParameterType == typeof(IFormFile) ||
                             typeof(List<IFormFile>).IsAssignableFrom(p.ParameterDescriptor.ParameterType)))
                .ToList();

            if (fileParams.Any())
            {
                foreach (var fileParam in fileParams)
                {
                    var fileParamName = fileParam.Name;

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
                                                    Format = "binary"
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
```

---

## 3. Estrutura do `ItemController`

Substitua o conteúdo do arquivo **ItemController.cs** pela seguinte estrutura:

```csharp
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrçamentoObra.Dto.Item;
using OrçamentoObra.Services.Interface;

namespace OrçamentoObra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemInterface _itemInterface;

        public ItemController(IItemInterface item)
        {
            _itemInterface = item;
        }

        [HttpPost]
        public async Task<IActionResult> CriarItem([FromBody] ItemCreateDTO itemCreateDTO)
        {
            var item = await _itemInterface.CriarItem(itemCreateDTO);
            return Ok(item);
        }

        [HttpGet("")]
        public IActionResult ListarItems()
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarItem([FromBody] ItemUpdateDTO itemUpdateDTO)
        {
            var item = await _itemInterface.EditarItem(itemUpdateDTO);
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverItem(int id)
        {
            var item = await _itemInterface.ExcluirItem(id);
            return Ok(item);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarItemPorId(int id)
        {
            var item = await _itemInterface.BuscarItemPorId(id);
            return Ok(item);
        }

        [HttpGet("categoria/{categoriaId}")]
        public async Task<IActionResult> ListarItemPorCategoria(int categoriaId)
        {
            var item = await _itemInterface.ListarItemPorCategoria(categoriaId);
            return Ok(item);
        }
    }
}
```

---

## 4. Configuração no `Program.cs`

Adicione o Swagger e registre o filtro `FileUploadOperationFilter` no `Program.cs`:

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrçamentoObra.Data;
using OrçamentoObra.Profiles;
using OrçamentoObra.Services;
using OrçamentoObra.Services.Interface;
using OrçamentoObra.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configurar DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar AutoMapper
builder.Services.AddAutoMapper(typeof(CategoriaProfile).Assembly);

// Configurar services
builder.Services.AddScoped<ICategoriaInterface, CategoriaService>();
builder.Services.AddScoped<IClienteInterface, ClienteService>();
builder.Services.AddScoped<IEmpresaInterface, EmpresaService>();
builder.Services.AddScoped<IItemInterface, ItemService>();
builder.Services.AddScoped<IObraInterface, ObraService>();
builder.Services.AddScoped<IOrcamentoInterface, OrcamentoService>();
builder.Services.AddScoped<IArquivoInterface, ArquivoService>();

// Configurar Swagger / Swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minha API",
        Version = "v1",
        Description = "API de exemplo usando Swagger",
        Contact = new OpenApiContact
        {
            Name = "Gustavo",
            Email = "gustavojesus79@gmail.com"
        }
    });

    // Linkar XML comments
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

    // Registrar filtro para upload de arquivos
    c.OperationFilter<FileUploadOperationFilter>();
});

var app = builder.Build();

// Configure o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API Swagger v1");
        c.RoutePrefix = string.Empty; // Swagger na raiz
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

```

---

## 5. Testando no Swagger

Agora você já pode executar o projeto.  
O Swagger estará disponível e funcionando corretamente:  

![Swagger exemplo](https://github.com/ralfslima/swagger_gustavo/blob/master/imagem_swagger.png)

---

✅ Pronto! O Swagger está configurado no seu projeto.  
Bons estudos e sucesso 🚀
