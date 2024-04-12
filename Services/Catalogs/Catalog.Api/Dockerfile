#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Catalogs/Catalog.Api/Catalog.Api.csproj", "Services/Catalogs/Catalog.Api/"]
COPY ["Services/Catalogs/Catalog.Common/Catalog.Common.csproj", "Services/Catalogs/Catalog.Common/"]
COPY ["Services/Catalogs/Catalog.Persistance/Catalog.Persistance.csproj", "Services/Catalogs/Catalog.Persistance/"]
COPY ["Services/Catalogs/Catalog.Application/Catalog.Application.csproj", "Services/Catalogs/Catalog.Application/"]
COPY ["Services/Catalogs/Catalog.Domain/Catalog.Domain.csproj", "Services/Catalogs/Catalog.Domain/"]
RUN dotnet restore "./Services/Catalogs/Catalog.Api/./Catalog.Api.csproj"
COPY . .
WORKDIR "/src/Services/Catalogs/Catalog.Api"
RUN dotnet build "./Catalog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Catalog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Api.dll"]