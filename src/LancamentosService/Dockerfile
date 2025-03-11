FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/LancamentosService/LancamentosService.csproj", "LancamentosService/"]
RUN dotnet restore "LancamentosService/LancamentosService.csproj"
COPY src/LancamentosService/. LancamentosService/.
WORKDIR "/src/LancamentosService"
RUN dotnet build "LancamentosService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LancamentosService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LancamentosService.dll"]