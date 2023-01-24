#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ReadApi/ReadApi.csproj", "src/ReadApi/"]
RUN dotnet restore "src/ReadApi/ReadApi.csproj"
COPY . .
WORKDIR "/src/src/ReadApi"
RUN dotnet build "ReadApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReadApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReadApi.dll"]