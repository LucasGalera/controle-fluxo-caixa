FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/ConsolidadoDiarioService/ConsolidadoDiarioService.csproj", "ConsolidadoDiarioService/"]
RUN dotnet restore "ConsolidadoDiarioService/ConsolidadoDiarioService.csproj"
COPY src/ConsolidadoDiarioService/. ConsolidadoDiarioService/.
WORKDIR "/src/ConsolidadoDiarioService"
RUN dotnet build "ConsolidadoDiarioService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsolidadoDiarioService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsolidadoDiarioService.dll"]