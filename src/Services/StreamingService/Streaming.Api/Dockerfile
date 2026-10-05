# Сборка из корня репозитория:
#   docker build -f src/Services/StreamingService/Streaming.Api/Dockerfile .
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /repo

COPY Directory.Build.props ./
COPY src/BuildingBlocks/SharedKernel/*.csproj src/BuildingBlocks/SharedKernel/
COPY src/BuildingBlocks/SharedKernel.AspNetCore/*.csproj src/BuildingBlocks/SharedKernel.AspNetCore/
COPY src/BuildingBlocks/SharedKernel.Application/*.csproj src/BuildingBlocks/SharedKernel.Application/
COPY src/BuildingBlocks/SharedKernel.Persistence/*.csproj src/BuildingBlocks/SharedKernel.Persistence/
COPY src/Services/StreamingService/Streaming.Domain/*.csproj src/Services/StreamingService/Streaming.Domain/
COPY src/Services/StreamingService/Streaming.Application/*.csproj src/Services/StreamingService/Streaming.Application/
COPY src/Services/StreamingService/Streaming.Infrastructure/*.csproj src/Services/StreamingService/Streaming.Infrastructure/
COPY src/Services/StreamingService/Streaming.Api/*.csproj src/Services/StreamingService/Streaming.Api/
RUN dotnet restore src/Services/StreamingService/Streaming.Api/Streaming.Api.csproj

COPY src/BuildingBlocks/ src/BuildingBlocks/
COPY src/Services/StreamingService/ src/Services/StreamingService/
RUN dotnet publish src/Services/StreamingService/Streaming.Api/Streaming.Api.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Streaming.Api.dll"]
