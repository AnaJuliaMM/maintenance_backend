FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY GatewayAPI/GatewayAPI.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app GatewayAPI/GatewayAPI.csproj

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "GatewayAPI.dll"]