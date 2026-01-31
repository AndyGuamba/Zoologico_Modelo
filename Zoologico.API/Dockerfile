FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:${PORT}

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["ZoologicoMVC/ZoologicoMVC.csproj", "ZoologicoMVC/"]
COPY ["Zoologico.API/Zoologico.API.csproj", "Zoologico.API/"]
COPY ["Zoologico_Modelo/Zoologico_Modelo.csproj", "Zoologico_Modelo/"]
 
RUN dotnet restore "ZoologicoMVC/ZoologicoMVC.csproj"
COPY . .

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR "/src/ZoologicoMVC"
RUN dotnet publish "ZoologicoMVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZoologicoMVC.dll"]
