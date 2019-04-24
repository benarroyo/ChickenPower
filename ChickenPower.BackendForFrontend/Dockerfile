FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ChickenPower.BackendForFrontend/ChickenPower.BackendForFrontend.csproj", "ChickenPower.BackendForFrontend/"]
COPY ["ChickenPower.Messaging/ChickenPower.Messaging.csproj", "ChickenPower.Messaging/"]
RUN dotnet restore "ChickenPower.BackendForFrontend/ChickenPower.BackendForFrontend.csproj"
COPY . .
WORKDIR "/src/ChickenPower.BackendForFrontend"
RUN dotnet build "ChickenPower.BackendForFrontend.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ChickenPower.BackendForFrontend.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ChickenPower.BackendForFrontend.dll"]
