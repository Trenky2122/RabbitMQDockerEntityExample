#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RabbitMQDockerEntityExample.Presentation/RabbitMQDockerEntityExample.Presentation.csproj", "RabbitMQDockerEntityExample.Presentation/"]
RUN dotnet restore "./RabbitMQDockerEntityExample.Presentation/./RabbitMQDockerEntityExample.Presentation.csproj"
COPY . .
WORKDIR "/src/RabbitMQDockerEntityExample.Presentation"
RUN dotnet build "./RabbitMQDockerEntityExample.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RabbitMQDockerEntityExample.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitMQDockerEntityExample.Presentation.dll"]