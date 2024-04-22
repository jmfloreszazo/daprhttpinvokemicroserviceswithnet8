#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["apitwo/apitwo.csproj", "apitwo/"]
RUN dotnet restore "apitwo/apitwo.csproj"
COPY . .
WORKDIR "/src/apitwo"
RUN dotnet build "apitwo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "apitwo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "apitwo.dll"]