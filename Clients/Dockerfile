# Etapa base: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar csproj primeiro e restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar todos os arquivos do projeto
COPY . ./

# Publicar em release para a pasta /app
RUN dotnet publish -c Release -o /app

# Etapa final: runtime
FROM base AS final
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "Clients.dll"]
