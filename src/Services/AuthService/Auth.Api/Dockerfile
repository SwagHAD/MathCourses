# Сборка из корня репозитория:
#   docker build -f src/Services/AuthService/Auth.Api/Dockerfile .
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /repo

# Сначала только файлы проектов — чтобы restore кэшировался
COPY Directory.Build.props ./
COPY src/BuildingBlocks/Infrastructure.Events/*.csproj src/BuildingBlocks/Infrastructure.Events/
COPY src/Services/AuthService/Auth.Domain/*.csproj src/Services/AuthService/Auth.Domain/
COPY src/Services/AuthService/Auth.Application/*.csproj src/Services/AuthService/Auth.Application/
COPY src/Services/AuthService/Auth.Infrastructure/*.csproj src/Services/AuthService/Auth.Infrastructure/
COPY src/Services/AuthService/Auth.Infrastructure.Messaging/*.csproj src/Services/AuthService/Auth.Infrastructure.Messaging/
COPY src/Services/AuthService/Auth.Seeds/*.csproj src/Services/AuthService/Auth.Seeds/
COPY src/Services/AuthService/Auth.Api/*.csproj src/Services/AuthService/Auth.Api/
RUN dotnet restore src/Services/AuthService/Auth.Api/Auth.Api.csproj

COPY src/BuildingBlocks/ src/BuildingBlocks/
COPY src/Services/AuthService/ src/Services/AuthService/
RUN dotnet publish src/Services/AuthService/Auth.Api/Auth.Api.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Auth.Api.dll"]
