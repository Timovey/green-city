#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.sln ./
COPY ["back/Common/DataContracts/DispatcherService.Contracts/", "Common/DataContracts/DispatcherService.Contracts/"]
COPY ["back/Common/DataContracts/TransportContracts/", "Common/DataContracts/TransportContracts/"]
COPY ["back/Common/DataContracts/LogistService.Contracts/", "Common/DataContracts/LogistService.Contracts/"]
COPY ["back/Common/DataContracts/AuthContracts/", "Common/DataContracts/AuthContracts/"]
COPY ["back/Common/DataContracts/ReportService.Contracts/", "Common/DataContracts/ReportService.Contracts/"]
COPY ["back/Common/DataContracts/MissionService.Contracts/", "Common/DataContracts/MissionService.Contracts/"]
COPY ["back/DispatcherService/", "DispatcherService/"]
COPY ["back/nuget.config", ""]
RUN dotnet restore "DispatcherService/DispatcherService.Main/DispatcherService.Main.csproj"
WORKDIR "/src/DispatcherService/DispatcherService.Main"
RUN dotnet build "DispatcherService.Main.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DispatcherService.Main.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DispatcherService.Main.dll"]