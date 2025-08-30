# Imagem base do .NET SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia csproj e restaura dependências
COPY *.csproj .
RUN dotnet restore

# Copia todo o código e faz build
COPY . .
RUN dotnet publish -c Release -o out

# Runtime mais leve para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "clients.dll"]
