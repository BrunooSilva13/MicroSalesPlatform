using Clients.Repositories;
using Clients.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 📌 Conexão vinda do docker-compose.yml
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 📌 Registrando dependências (Scoped → 1 instância por requisição HTTP)
builder.Services.AddScoped<ClientRepository>(_ => new ClientRepository(connectionString));
builder.Services.AddScoped<ClientService>();

builder.Services.AddControllers();

// 📌 Adicionando e personalizando Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Clients API",
        Version = "v1",
        Description = "API responsável pelo gerenciamento de clientes no sistema de vendas."
    });
});

var app = builder.Build();

// 📌 Configurando Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clients API V1");
        c.RoutePrefix = string.Empty; // Swagger na raiz → http://localhost:5000/
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
