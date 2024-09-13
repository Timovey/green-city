#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.sln ./
COPY ["back/Common/DataContracts/MissionService.Contracts/", "Common/DataContracts/MissionService.Contracts/"]
COPY ["back/Common/DataContracts/SDContracts/", "Common/DataContracts/SDContracts/"]
COPY ["back/Common/DataContracts/ScheduleService.Contracts/", "Common/DataContracts/ScheduleService.Contracts/"]
COPY ["back/Common/DataContracts/LogistService.Contracts/", "Common/DataContracts/LogistService.Contracts/"]
COPY ["back/MissionService/", "MissionService/"]
COPY ["back/nuget.config", ""]
RUN dotnet restore "MissionService/MissionService.Main/MissionService.Main.csproj"
WORKDIR "/src/MissionService/MissionService.Main"
RUN dotnet build "MissionService.Main.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MissionService.Main.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MissionService.Main.dll"]