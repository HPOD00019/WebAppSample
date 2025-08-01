FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80 #port 80 for HTTP


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src


COPY ["Microservices/AuthService/AuthService.Api/AuthService.Api.csproj", "AuthService.Api/"]
COPY ["Microservices/AuthService/AuthService.Application/AuthService.Application.csproj", "AuthService.Application/"]
COPY ["Microservices/AuthService/AuthService.Domain/AuthService.Domain.csproj", "AuthService.Domain/"]
COPY ["Microservices/AuthService/AuthService.Domain/AuthService.Infrastructure/AuthService.Infrastructure.csproj", "AuthService.Infrastructure/"]


RUN dotnet restore "AuthService.API/AuthService.Api.csproj"

COPY . .

