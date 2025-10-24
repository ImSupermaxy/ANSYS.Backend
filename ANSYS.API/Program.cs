using ANSYS.API;
using ANSYS.Application.Dependency;
using ANSYS.Dependency.Infrastructure;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ANSYS API",
        Description = "Uma ASP.NET Core Web API para gerenciamento de funcionarios, pedidos, menu e clientes de um restaurante. \n\n\nObservação: Não esqueça de olhar o README no nosso github (visualize em termos de serviço). \n\nCaso o banco do entity framework não esteja funcionando, utilize o banco de dados em tempo de execução!\n\nAtualize em: ANSYS.Infrastructure -> Dependency -> DependencyInjection.cs -> linha 53, mude de \"false\" para \"true\"",
        TermsOfService = new Uri("https://github.com/ImSupermaxy/ANSYS.Backend"),
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
