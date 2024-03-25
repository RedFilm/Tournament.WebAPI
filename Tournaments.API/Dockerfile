FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Tournaments.API/Tournaments.API.csproj", "Tournaments.API/"]
COPY ["Tournaments.Application/Tournaments.Application.csproj", "Tournaments.Application/"]
COPY ["Tournaments.Domain/Tournaments.Domain.csproj", "Tournaments.Domain/"]
COPY ["Tournaments.Persistence/Tournaments.Persistence.csproj", "Tournaments.Persistence/"]
RUN dotnet restore "./Tournaments.API/./Tournaments.API.csproj"
COPY . .
WORKDIR "/src/Tournaments.API"
RUN dotnet build "./Tournaments.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Tournaments.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tournaments.API.dll"]