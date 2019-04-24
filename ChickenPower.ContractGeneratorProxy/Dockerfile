FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["../ChickenPower.ContractGeneratorProxy/ChickenPower.ContractGeneratorProxy.csproj", "../ChickenPower.ContractGeneratorProxy/"]
COPY ["ChickenPower.Common/ChickenPower.Common.csproj", "ChickenPower.Common/"]
COPY ["ChickenPower.Messaging/ChickenPower.Messaging.csproj", "ChickenPower.Messaging/"]
RUN dotnet restore "../ChickenPower.ContractGeneratorProxy/ChickenPower.ContractGeneratorProxy.csproj"
COPY . .
WORKDIR "/src/../ChickenPower.ContractGeneratorProxy"
RUN dotnet build "ChickenPower.ContractGeneratorProxy.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ChickenPower.ContractGeneratorProxy.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ChickenPower.ContractGeneratorProxy.dll"]
