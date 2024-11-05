FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY UsersAPI/AuthMicroservice.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app UsersAPI/AuthMicroservice.csproj

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AuthMicroservice.dll"]