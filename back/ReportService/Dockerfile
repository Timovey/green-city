#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.sln ./
COPY ["back/Common/DataContracts/ReportService.Contracts/", "Common/DataContracts/ReportService.Contracts/"]
COPY ["back/ReportService/", "ReportService/"]
COPY ["back/nuget.config", ""]
RUN dotnet restore "ReportService/ReportService.Main/ReportService.Main.csproj"
WORKDIR "/src/ReportService/ReportService.Main"
RUN dotnet build "ReportService.Main.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportService.Main.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportService.Main.dll"]