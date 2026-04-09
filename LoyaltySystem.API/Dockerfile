FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LoyaltySystem.API/LoyaltySystem.API.csproj", "LoyaltySystem.API/"]
COPY ["LoyaltySystem.Application/LoyaltySystem.Application.csproj", "LoyaltySystem.Application/"]
COPY ["LoyaltySystem.Domain/LoyaltySystem.Domain.csproj", "LoyaltySystem.Domain/"]
COPY ["LoyaltySystem.Infrastructure/LoyaltySystem.Infrastructure.csproj", "LoyaltySystem.Infrastructure/"]
RUN dotnet restore "LoyaltySystem.API/LoyaltySystem.API.csproj"
COPY . .
WORKDIR "/src/LoyaltySystem.API"
RUN dotnet build "./LoyaltySystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LoyaltySystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LoyaltySystem.API.dll"]
