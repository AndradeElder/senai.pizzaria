#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProjetoEmTresCamadas.Pizzaria.Mvc/ProjetoEmTresCamadas.Pizzaria.Mvc.csproj", "ProjetoEmTresCamadas.Pizzaria.Mvc/"]
COPY ["ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio/ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio.csproj", "ProjetoEmTresCamadas.Pizzaria.RegraDeNegocio/"]
COPY ["ProjetoEmTresCamadas.Pizzaria.DAO/ProjetoEmTresCamadas.Pizzaria.DAO.csproj", "ProjetoEmTresCamadas.Pizzaria.DAO/"]
RUN dotnet restore "./ProjetoEmTresCamadas.Pizzaria.Mvc/./ProjetoEmTresCamadas.Pizzaria.Mvc.csproj"
COPY . .
WORKDIR "/src/ProjetoEmTresCamadas.Pizzaria.Mvc"
RUN dotnet build "./ProjetoEmTresCamadas.Pizzaria.Mvc.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProjetoEmTresCamadas.Pizzaria.Mvc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjetoEmTresCamadas.Pizzaria.Mvc.dll"]