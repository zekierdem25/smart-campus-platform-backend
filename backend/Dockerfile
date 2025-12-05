FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["SmartCampus.API.csproj", "./"]
RUN dotnet restore "SmartCampus.API.csproj"
COPY . .
RUN dotnet build "SmartCampus.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartCampus.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartCampus.API.dll"]
