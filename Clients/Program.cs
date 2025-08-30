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

var app = builder.Build();

app.MapControllers();

app.Run();
