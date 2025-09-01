using Microsoft.EntityFrameworkCore;
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
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Minha API",
        Version = "v1",
        Description = "API de exemplo usando Swagger"
    });

    // Adiciona o filtro para tratar arquivos em multipart/form-data
    c.OperationFilter<FileUploadOperationFilter>();

    // Linkar XML comments (caso esteja usando)
    var xmlFile = "MinhaAPI.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
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
