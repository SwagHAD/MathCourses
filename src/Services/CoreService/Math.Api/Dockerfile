# Сборка из корня репозитория:
#   docker build -f src/Services/CoreService/Math.Api/Dockerfile .
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /repo

# Сначала только файлы проектов — чтобы restore кэшировался
COPY Directory.Build.props ./
COPY src/BuildingBlocks/Infrastructure.Events/*.csproj src/BuildingBlocks/Infrastructure.Events/
COPY src/Services/CoreService/Math.Domain/*.csproj src/Services/CoreService/Math.Domain/
COPY src/Services/CoreService/Math.Application/*.csproj src/Services/CoreService/Math.Application/
COPY src/Services/CoreService/Math.Infrastructure/*.csproj src/Services/CoreService/Math.Infrastructure/
COPY src/Services/CoreService/Math.Infrastructure.Messaging/*.csproj src/Services/CoreService/Math.Infrastructure.Messaging/
COPY src/Services/CoreService/Math.Api/*.csproj src/Services/CoreService/Math.Api/
RUN dotnet restore src/Services/CoreService/Math.Api/Math.Api.csproj

COPY src/BuildingBlocks/ src/BuildingBlocks/
COPY src/Services/CoreService/ src/Services/CoreService/
RUN dotnet publish src/Services/CoreService/Math.Api/Math.Api.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Math.Api.dll"]
