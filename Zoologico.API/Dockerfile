# 1. Base para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# Exponemos el puerto 505 que es el que detectamos en tus logs
EXPOSE 505
ENV ASPNETCORE_URLS=http://+:505

# 2. Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos los archivos de proyecto para restaurar dependencias
COPY ["ZoologicoMVC/ZoologicoMVC.csproj", "ZoologicoMVC/"]
COPY ["Zoologico.API/Zoologico.API.csproj", "Zoologico.API/"]
COPY ["Zoologico_Modelo/Zoologico_Modelo.csproj", "Zoologico_Modelo/"]

# Restauramos basándonos en el proyecto MVC
RUN dotnet restore "ZoologicoMVC/ZoologicoMVC.csproj"

# Copiamos todo el código fuente
COPY . .

# Compilamos el proyecto MVC
WORKDIR "/src/ZoologicoMVC"
RUN dotnet build "ZoologicoMVC.csproj" -c Release -o /app/build

# 3. Etapa de publicación
FROM build AS publish
RUN dotnet publish "ZoologicoMVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 4. Configuración final para Render
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Punto de entrada: ahora ejecutamos el MVC
ENTRYPOINT ["dotnet", "ZoologicoMVC.dll"]