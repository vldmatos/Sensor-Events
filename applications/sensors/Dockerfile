
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["applications/sensors/Sensors.csproj", "applications/sensors/"]
RUN dotnet restore "applications/sensors/Sensors.csproj"
COPY . .
WORKDIR "/src/applications/sensors"
RUN dotnet build "Sensors.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sensors.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sensors.dll"]
