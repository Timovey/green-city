#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.sln ./
COPY ["back/Common/DataContracts/TransportContracts/", "Common/DataContracts/TransportContracts/"]
COPY ["back/TransportService/", "TransportService/"]
COPY ["back/nuget.config", ""]
RUN dotnet restore "TransportService/TransportService.Main/TransportService.Main.csproj"
WORKDIR "/src/TransportService/TransportService.Main"
RUN dotnet build "TransportService.Main.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TransportService.Main.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransportService.Main.dll"]