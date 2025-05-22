FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 22789

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["QuickMock/QuickMock.csproj", "QuickMock/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Core/Core.csproj", "Core/"]
RUN dotnet restore "QuickMock/QuickMock.csproj"
COPY . .
WORKDIR "/src/QuickMock"
RUN dotnet build "./QuickMock.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QuickMock.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:22789;https://+:22790
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuickMock.dll"]
