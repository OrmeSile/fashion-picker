FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5106

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FashionPicker.Bff.Web/FashionPicker.Bff.Web.csproj", "FashionPicker.Bff.Web/"]
RUN dotnet restore "FashionPicker.Bff.Web/FashionPicker.Bff.Web.csproj"
COPY . .
WORKDIR "/src/FashionPicker.Bff.Web"
RUN dotnet build "./FashionPicker.Bff.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FashionPicker.Bff.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FashionPicker.Bff.Web.dll"]
