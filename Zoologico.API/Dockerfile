# Fase base para ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
# Exponemos el puerto 505 que detectamos en tus logs anteriores
EXPOSE 505
ENV ASPNETCORE_URLS=http://+:505

# Fase de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1. Copiamos los archivos de proyecto (.csproj) de toda la solución
COPY ["ZoologicoMVC/ZoologicoMVC.csproj", "ZoologicoMVC/"]
COPY ["Zoologico.API/Zoologico.API.csproj", "Zoologico.API/"]
COPY ["Zoologico_Modelo/Zoologico_Modelo.csproj", "Zoologico_Modelo/"]

# 2. Restauramos las dependencias
RUN dotnet restore "ZoologicoMVC/ZoologicoMVC.csproj"

# 3. Copiamos el resto del código fuente
COPY . .

# 4. Fase de publicación (Aquí corregimos el error de archivos duplicados)
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Eliminamos los archivos appsettings de la API para que no choquen con los de MVC
RUN rm -f /src/Zoologico.API/appsettings.json
RUN rm -f /src/Zoologico.API/appsettings.Development.json

WORKDIR "/src/ZoologicoMVC"
RUN dotnet publish "ZoologicoMVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final de producción
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# El punto de entrada principal ahora es el proyecto MVC
ENTRYPOINT ["dotnet", "ZoologicoMVC.dll"]