#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY *.sln ./
COPY ["back/Common/DataContracts/", "Common/DataContracts/"]
COPY ["back/ExternalApiService/", "ExternalApiService/"]
COPY ["back/nuget.config", ""]
RUN dotnet restore "ExternalApiService/ExternalApiService/ExternalApiService.csproj"
WORKDIR "/src/ExternalApiService/ExternalApiService"
RUN dotnet build "ExternalApiService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExternalApiService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExternalApiService.dll"]