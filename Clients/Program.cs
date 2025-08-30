using Clients.Repositories;
using Clients.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Conexão vinda do docker-compose.yml
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registrando dependências
builder.Services.AddSingleton<ClientRepository>(_ => new ClientRepository(connectionString));
builder.Services.AddSingleton<ClientService>();

builder.Services.AddControllers();

// ⚡ Adicionando Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ⚡ Configurando Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clients API V1");
        c.RoutePrefix = string.Empty; // Swagger na raiz http://localhost:5000/
    });
}

app.MapControllers();

app.Run();
