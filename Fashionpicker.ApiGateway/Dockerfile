FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Fashionpicker.ApiGateway/Fashionpicker.ApiGateway.csproj", "Fashionpicker.ApiGateway/"]
RUN dotnet restore "Fashionpicker.ApiGateway/Fashionpicker.ApiGateway.csproj"
COPY . .
WORKDIR "/src/Fashionpicker.ApiGateway"
RUN dotnet build "./Fashionpicker.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Fashionpicker.ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fashionpicker.ApiGateway.dll"]
