FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src
COPY ["PokeSaveRomManager.Api/PokeSaveRomManager.Api.csproj", "PokeSaveRomManager.Api/"]
COPY ["PokeSaveRomManager.Data/PokeSaveRomManager.Data.csproj", "PokeSaveRomManager.Data/"]
COPY ["PokeSaveRomManager.Parser/PokeSaveRomManager.Parser.csproj", "PokeSaveRomManager.Parser/"]
RUN dotnet restore "PokeSaveRomManager.Api/PokeSaveRomManager.Api.csproj"
COPY . .
WORKDIR "/src/PokeSaveRomManager.Api"
RUN dotnet build "PokeSaveRomManager.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PokeSaveRomManager.Api.csproj" -c Release -o /app/publish /p:PublishReadyToRun=false /p:PublishSingleFile=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "PokeSaveRomManager.Api.dll"]