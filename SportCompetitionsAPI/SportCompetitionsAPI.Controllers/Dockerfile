#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SportCompetitionsAPI.Controllers/SportCompetitionsAPI.Controllers.csproj", "SportCompetitionsAPI.Controllers/"]
COPY ["SportCompetitionsAPI.Domain/SportCompetitionsAPI.Domain.csproj", "SportCompetitionsAPI.Domain/"]
COPY ["SportCompetitionsAPI.Service/SportCompetitionsAPI.Service.csproj", "SportCompetitionsAPI.Service/"]
RUN dotnet restore "./SportCompetitionsAPI.Controllers/./SportCompetitionsAPI.Controllers.csproj"
COPY . .
WORKDIR "/src/SportCompetitionsAPI.Controllers"
RUN dotnet build "./SportCompetitionsAPI.Controllers.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build as test
WORKDIR "/src/SportCompetitionsAPI.Test"
RUN dotnet test

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SportCompetitionsAPI.Controllers.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SportCompetitionsAPI.Controllers.dll"]