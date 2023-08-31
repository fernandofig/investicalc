FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:18-alpine AS frontend
WORKDIR /fe-src
COPY ["InvestmentCalculator.WebFront/", "."]
RUN npm install
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["InvestmentCalculator.WebAPI/InvestmentCalculator.WebAPI.csproj", "InvestmentCalculator.WebAPI/"]
COPY ["InvestmentCalculator.Domain/InvestmentCalculator.Domain.csproj", "InvestmentCalculator.Domain/"]
COPY ["InvestmentCalculator.Infrastructure/InvestmentCalculator.Infrastructure.csproj", "InvestmentCalculator.Infrastructure/"]
COPY ["InvestmentCalculator.Services/InvestmentCalculator.Services.csproj", "InvestmentCalculator.Services/"]
RUN dotnet restore "InvestmentCalculator.WebAPI/InvestmentCalculator.WebAPI.csproj"
COPY . .
WORKDIR "/src/InvestmentCalculator.WebAPI"
RUN dotnet build "InvestmentCalculator.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InvestmentCalculator.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false /p:PublishProfile=ContainerProfile

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=frontend /fe-src/dist ./frontend/
ENTRYPOINT ["dotnet", "InvestmentCalculator.WebAPI.dll"]