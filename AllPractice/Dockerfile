#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AllPractice/AllPractice.csproj", "AllPractice/"]
RUN dotnet restore "AllPractice/AllPractice.csproj"
COPY . .
WORKDIR "/src/AllPractice"
RUN dotnet build "AllPractice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AllPractice.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV AppSetting__URL__Npgsql="Host=host.docker.internal;Port=6060;Database=1234;Username=1234;Password=1234;Persist Security Info=True"
ENTRYPOINT ["dotnet", "AllPractice.dll"]