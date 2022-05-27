#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/api gateways/TC.Bff.Crud/TC.Bff.Crud.csproj", "src/api gateways/TC.Bff.Crud/"]
RUN dotnet restore "src/api gateways/TC.Bff.Crud/TC.Bff.Crud.csproj"
COPY . .
WORKDIR "/src/src/api gateways/TC.Bff.Crud"
RUN dotnet build "TC.Bff.Crud.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TC.Bff.Crud.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TC.Bff.Crud.dll"]