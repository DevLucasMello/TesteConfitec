#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["./services/TC.Usuarios.API/TC.Usuarios.API.csproj", "services/TC.Usuarios.API/"]
COPY ["./building blocks/TC.Core/TC.Core.csproj", "building blocks/TC.Core/"]
COPY ["./building blocks/TC.WebAPI.Core/TC.WebAPI.Core.csproj", "building blocks/TC.WebAPI.Core/"]
COPY ["./services/TC.Usuarios.Application/TC.Usuarios.Application.csproj", "services/TC.Usuarios.Application/"]
COPY ["./services/TC.Usuarios.Domain/TC.Usuarios.Domain.csproj", "services/TC.Usuarios.Domain/"]
COPY ["./services/TC.Usuarios.Infra/TC.Usuarios.Infra.csproj", "services/TC.Usuarios.Infra/"]
RUN dotnet restore "services/TC.Usuarios.API/TC.Usuarios.API.csproj"
COPY . .
WORKDIR "services/TC.Usuarios.API"
RUN dotnet build "TC.Usuarios.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TC.Usuarios.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TC.Usuarios.API.dll"]