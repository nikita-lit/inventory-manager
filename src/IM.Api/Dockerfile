FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "Directory.Packages.props", "./"]
COPY ["src/IM.Api/IM.Api.csproj", "src/IM.Api/"]
COPY ["src/IM.Core/IM.Core.csproj", "src/IM.Core/"]
COPY ["src/IM.Data/IM.Data.csproj", "src/IM.Data/"]
RUN dotnet restore "src/IM.Api/IM.Api.csproj"
COPY ["src/IM.Api/", "src/IM.Api/"]
COPY ["src/IM.Core/", "src/IM.Core/"]
COPY ["src/IM.Data/", "src/IM.Data/"]
WORKDIR "/src/src/IM.Api"
RUN dotnet build "./IM.Api.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./IM.Api.csproj" -c $BUILD_CONFIGURATION --no-restore -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IM.Api.dll"]
