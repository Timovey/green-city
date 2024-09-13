#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.sln ./
COPY ["back/Common/DataContracts/ScheduleService.Contracts/", "Common/DataContracts/ScheduleService.Contracts/"]
COPY ["back/ScheduleService/", "ScheduleService/"]
COPY ["back/nuget.config", ""]
RUN dotnet restore "ScheduleService/ScheduleService.Main/ScheduleService.Main.csproj"
WORKDIR "/src/ScheduleService/ScheduleService.Main"
RUN dotnet build "ScheduleService.Main.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ScheduleService.Main.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ScheduleService.Main.dll"]