# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FIAP.6NETT-GRUPO31.Service/ProcessingContact.API.csproj", "FIAP.6NETT-GRUPO31.Service/"]
COPY ["FIAP.6NETT-GRUPO31.Application/ProcessingContact.Application.csproj", "FIAP.6NETT-GRUPO31.Application/"]
COPY ["FIAP.6NETT-GRUPO31.Domain/ProcessingContact.Domain.csproj", "FIAP.6NETT-GRUPO31.Domain/"]
COPY ["FIAP.6NETT-GRUPO31.Infra.Data/ProcessingContact.Infra.Data.csproj", "FIAP.6NETT-GRUPO31.Infra.Data/"]
RUN dotnet restore "./FIAP.6NETT-GRUPO31.Service/ProcessingContact.API.csproj"
COPY . .
WORKDIR "/src/FIAP.6NETT-GRUPO31.Service"
RUN dotnet build "./ProcessingContact.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProcessingContact.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProcessingContact.API.dll"]