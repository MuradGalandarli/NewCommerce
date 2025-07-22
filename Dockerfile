# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["Presentation/NewCommerce.Api/NewCommerce.Api.csproj", "Presentation/NewCommerce.Api/"]
COPY ["Core/NewCommerce.Application/NewCommerce.Application.csproj", "Core/NewCommerce.Application/"]
COPY ["Core/NewCommerce.Domain/NewCommerce.Domain.csproj", "Core/NewCommerce.Domain/"]
COPY ["Infrastructure/NewCommerce.Infrastructure/NewCommerce.Infrastructure.csproj", "Infrastructure/NewCommerce.Infrastructure/"]
COPY ["Infrastructure/NewCommerce.Persistence/NewCommerce.Persistence.csproj", "Infrastructure/NewCommerce.Persistence/"]
COPY ["Infrastructure/NewCommerce.SignalR/NewCommerce.SignalR.csproj", "Infrastructure/NewCommerce.SignalR/"]
RUN dotnet restore "Presentation/NewCommerce.Api/NewCommerce.Api.csproj"

COPY . .
WORKDIR "/src/Presentation/NewCommerce.Api"
RUN dotnet build "NewCommerce.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NewCommerce.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage (use aspnet runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewCommerce.Api.dll"]
