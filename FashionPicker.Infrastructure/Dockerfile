FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FashionPicker.Infrastructure/FashionPicker.Infrastructure.csproj", "FashionPicker.Infrastructure/"]
COPY ["FashionPicker.Core/FashionPicker.Core.csproj", "FashionPicker.Core/"]
RUN dotnet restore "FashionPicker.Infrastructure/FashionPicker.Infrastructure.csproj"
COPY . .
WORKDIR "/src/FashionPicker.Infrastructure"
RUN dotnet build "./FashionPicker.Infrastructure.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FashionPicker.Infrastructure.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FashionPicker.Infrastructure.dll"]
