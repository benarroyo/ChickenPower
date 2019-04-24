FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ChickenPower.PricingProxy/ChickenPower.PricingProxy.csproj", "ChickenPower.PricingProxy/"]
COPY ["ChickenPower.Messaging/ChickenPower.Messaging.csproj", "ChickenPower.Messaging/"]
RUN dotnet restore "ChickenPower.PricingProxy/ChickenPower.PricingProxy.csproj"
COPY . .
WORKDIR "/src/ChickenPower.PricingProxy"
RUN dotnet build "ChickenPower.PricingProxy.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ChickenPower.PricingProxy.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ChickenPower.PricingProxy.dll"]
