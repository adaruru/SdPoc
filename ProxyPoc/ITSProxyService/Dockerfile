#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ITSProxyService/ITSProxyService.csproj", "ITSProxyService/"]
RUN dotnet restore "ITSProxyService/ITSProxyService.csproj"
COPY . .
WORKDIR "/src/ITSProxyService"
RUN dotnet build "ITSProxyService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ITSProxyService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ITSProxyService.dll"]