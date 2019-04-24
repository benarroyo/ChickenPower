FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ChickenPower.StateSaga/ChickenPower.StateSaga.csproj", "ChickenPower.StateSaga/"]
COPY ["ChickenPower.Persistence/ChickenPower.Persistence.csproj", "ChickenPower.Persistence/"]
COPY ["ChickenPower.Messaging/ChickenPower.Messaging.csproj", "ChickenPower.Messaging/"]
RUN dotnet restore "ChickenPower.StateSaga/ChickenPower.StateSaga.csproj"
COPY . .
WORKDIR "/src/ChickenPower.StateSaga"
RUN dotnet build "ChickenPower.StateSaga.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ChickenPower.StateSaga.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ChickenPower.StateSaga.dll"]
